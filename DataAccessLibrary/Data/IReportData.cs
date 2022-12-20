using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IReportData
    {
        List<RunnerReportDataModel> GetRunnerReportData(int runnerId, DateTime? dat_s, DateTime? dat_b);
        List<RunnerReportDataModel> GetRunnerHistoricData(int runnerId); 
        void UpdateRunnerReportData(RunnerReportDataModel reportdata, int runnerId);
        public List<ReportModel> CreateTrainingCycle(ReportModel trainingCylce);
        void DeleteCycle(string runner_id, int cycle_id); 
        List<ReportModel> GetTrainingCycles(string id);
        ReportModel GetTrainingCycle(string id);
        ReportWeekModel GetCurrentWeek(string id);
        List<ReportDataPlannedModel> GetPlannedReportList(string runnerId, int weeknum);
        void UpdatePlannedReports(ReportDataPlannedModel reportdata, string runnerId);
        List<CoachReportDataModel> GetCoachReportList(string runnerId, int weeknum);
        void UpdateCoachReport(CoachReportDataModel reportdata, string runnerId); 

    }
}