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
        private readonly IConfiguration _config;
        public string ConnectionStringName { get; set; } = "DefaultConnection";
        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        public List<T> LoadDataSP<T, U>(string sql, U parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> data = connection.Query<T>(sql, parameters).ToList();
                return data;
                
            }
        }

        public void SaveDataSP<T>(string sql, T parameters)
        {
            string connectionString = _config.GetConnectionString(ConnectionStringName);
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var data = connection.Execute(sql, parameters);
            }
        }

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
