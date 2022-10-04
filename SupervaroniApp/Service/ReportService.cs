using Dapper;
using Microsoft.OpenApi.Models;
using SupervaroniApp.IService;
using SupervaroniApp.Models;
using System.Data;
using System.Data.SqlClient;

namespace SupervaroniApp.Service
{
    
    public class ReportService : IReportService
    {
        public IConfiguration _Configuration { get; }
        public string _connectionString = "";
        List<Report> reportList = new List<Report>();
        Report report = new Report();
        
        public ReportService(IConfiguration configuration)
        {
            _Configuration = configuration;
            _connectionString = _Configuration.GetConnectionString("DefaultConnection");
        }
        string IReportService.Delete(int id)
        {

            throw new NotImplementedException();
        }

        Report IReportService.GetReportById(int id)
        {
            throw new NotImplementedException();
        }

        List<Report> IReportService.GetReports(String UserName)
        {
            reportList = new List<Report>();
            String uname = UserName;
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                if (con.State != ConnectionState.Open) con.Open();
                var reports = con.Query<Report>($"SELECT Datums, Ieplanots, Izpildits, Komentars FROM Atskaite WHERE UserName='{uname}'").ToList();
                if(reports != null && reports.Count > 0)
                {
                    reportList = reports;
                }
            }
            return reportList;
        }

        Report IReportService.SaveOrUpdate(Report report)
        {
            //report = new Report();
            //using(IDbConnection con = new SqlConnection(_connectionString))
            //{
            //    if (con.State != ConnectionState.Open) con.Open();

            //    int operationType = Convert.ToInt32(report.Id == 0 ? OperationType.Post : OperationType.Put); 

            //    if(operationType == 2)
            //    {
            //        //var report = con.Execute("INSERT INTO Atskaite (Datums, Ieplanots, Izpildits, Komentars) VALUES {}  ")
            //    }
            //    else if(operationType == 1)
            //    {
            //        var reports = con.Execute("Update Atskaites Set Datums = , Izpildits = ");
            //    }
                
            //}


            throw new NotImplementedException();
        }
    }
}
