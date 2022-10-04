using SupervaroniApp.Models;

namespace SupervaroniApp.IService
{
    public interface IReportService
    {
        Report GetReportById(int id);

        Report SaveOrUpdate(Report report);

        string Delete(int id);
        List<Report> GetReports(string username);
    }
}
