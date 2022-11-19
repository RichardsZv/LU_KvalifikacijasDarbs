
namespace DataAccessLibrary
{
    public interface ISqlDataAccess
    {
        string ConnectionStringName { get; set; }

        List<T> LoadData<T, U>(string sql, U parameters);
        void SaveData<T>(string sql, T parameters);
    }
}