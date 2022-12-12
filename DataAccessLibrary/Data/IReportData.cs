using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IReportData
    {
        List<RunnerReportDataModel> GetRunnerReportData(int runnerId, DateTime? dat_s, DateTime? dat_b);
        void UpdateRunnerReportData(RunnerReportDataModel reportdata, int runnerId);
        public List<ReportModel> CreateTrainingCycle(ReportModel trainingCylce);
        List<ReportModel> GetTrainingCycles(string id);

        public ReportWeekModel GetCycleWeekCount(int report_id); 
    }
}