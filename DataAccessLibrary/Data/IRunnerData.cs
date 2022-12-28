using AutoMapper.Configuration.Conventions;
using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IRunnerData
    {
        RunnerModel GetCurrentRunner(string uname);
        void SaveRunner(RunnerModel runner);
        public List<RunnerModel> GetRunners();
        public void SaveRunnerToCoach(int Id, string uname);
        public List<RunnerModel> GetCoachRunners(int id);
        public RunnerModel GetRunnerById(string id);
        public void AddTest(TestsModel test);
        void DeleteTest(string runner_id, int test_Id); 
        public List<TestsModel> GetTests(string runner_id);
        public void AddRace(RaceModel race);
        void DeleteRace(string runner_id, int race_id); 
        public List<RaceModel> GetRaces(string runner_id);
        public void AddInjury(InjuryModel injury);
        void DeleteInjury(string runner_id, int pb_id); 
        public List<InjuryModel> GetInjuries(string runner_id);
        public void AddPersonalBest(PersonalBestsModel pb);
        void DeletePb(string runner_id, int pb_id); 
        public List<PersonalBestsModel> GetPersonalBests(string runner_id);
        void RemoveTrainee(int runner_id, int coach_id);
        int GetStatus(int runnerId); 

    }
}