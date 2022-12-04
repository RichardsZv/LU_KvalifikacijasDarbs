using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IRunnerData
    {
        RunnerModel GetCurrentRunner(string uname);
        void SaveRunner(RunnerModel runner);
        public List<RunnerModel> GetRunners();
        public void SaveRunnerToCoach(int Id, string uname);
        public List<RunnerModel> GetCoachRunners(string uname);
        public RunnerModel GetRunnerById(string id);
    }
}