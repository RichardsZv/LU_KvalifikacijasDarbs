using DataAccessLibrary.Models;

namespace DataAccessLibrary.Data
{
    public interface IReportData
    {
        List<ReportDataModel> GetReportData(int runnerId, DateTime? dat_s, DateTime? dat_b);
        void UpdateReportData(ReportDataModel report, int runner_id);
        public void CreateTrainingCycle(ReportModel reportcycle);
    }
}