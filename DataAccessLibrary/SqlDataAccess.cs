using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary
{
    public class SqlDataAccess : ISqlDataAccess
    {
        //Konfigurācija
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "DefaultConnection";
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        //Datu ielāde ar vaicājumu
        public List<T> LoadData<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> data = connection.Query<T>(sql, parameters).ToList();
                return data;
                
            }
        }

        //Datu ielāde ar Sql procedūru 
        public List<T> LoadDataSP<T, U>(string storedProcedure, U parameters, string connString = "DefaultConnection") where T : class
        {
            string connectionString = _config.GetConnectionString(connString);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(storedProcedure, parameters, commandTimeout: 120,
                    commandType: CommandType.StoredProcedure).ToList();
            }
        }

        //Asinhrona datu ielāde ar Sql procedūru 
        public async Task<List<T>> LoadDataSPAsync<T, U>(string storedProcedure, U parameters, string connString)
        {
            string connectionString = _config.GetConnectionString(connString);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<T>(storedProcedure, parameters, commandTimeout: 120,
                    commandType: CommandType.StoredProcedure);
                return result.AsList();
            }
        }
        //Datu saglabāšana 
        public void SaveDataSP<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = connection.Execute(sql, parameters);
            }
        }
        //Vaicājuma izsaukums
        public List<T> Query<T>(string sqlQuery, string connString = "DefaultConnection") where T : class
        {
            string connectionString = _config.GetConnectionString(connString);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                return connection.Query<T>(sqlQuery).ToList();
            }
        }

    }
}
