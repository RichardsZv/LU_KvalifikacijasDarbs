
namespace DataAccessLibrary
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        List<T> LoadDataSP<T, U>(string sql, U parameters);
        void SaveDataSP<T>(string sql, T parameters);
        public List<T> Query<T>(string sqlQuery, string connString = "DefaultConnection") where T : class; 
    }
}