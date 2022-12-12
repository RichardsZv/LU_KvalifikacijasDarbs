using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IReportData
    {
        List<RunnerReportDataModel> GetRunnerReportData(int runnerId, DateTime? dat_s, DateTime? dat_b);
        void UpdateRunnerReportData(RunnerReportDataModel reportdata, int runnerId);
        public List<ReportModel> CreateTrainingCycle(ReportModel trainingCylce);
        List<ReportModel> GetTrainingCycles(string id);
        ReportModel GetTrainingCycle(string id);
        ReportWeekModel GetCurrentWeek(string id);
        List<ReportDataPlannedModel> GetPlannedReportList(string runnerId, int weeknum);
        void UpdatePlannedReports(ReportDataPlannedModel reportdata); 
    }
}