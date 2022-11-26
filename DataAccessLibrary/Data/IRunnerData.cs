using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IRunnerData
    {
        RunnerModel GetCurrentRunner(string uname);
        void SaveRunner(RunnerModel runner);
    }
}