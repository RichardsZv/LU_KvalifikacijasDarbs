
namespace DataAccessLibrary
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        List<T> LoadData<T, U>(string sql, U parameters);
        public List<T> LoadDataSP<T, U>(string storedProcedure, U parameters, string connString = "DefaultConnection") where T : class;
        public  Task<List<T>> LoadDataSPAsync<T, U>(string storedProcedure, U parameters, string connString); 
        void SaveDataSP<T>(string sql, T parameters);
        public List<T> Query<T>(string sqlQuery, string connString = "DefaultConnection") where T : class; 
    }
}