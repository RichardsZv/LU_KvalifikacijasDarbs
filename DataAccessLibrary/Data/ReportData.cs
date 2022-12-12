using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.ComponentModel;
using Microsoft.Extensions.Configuration;

namespace DataAccessLibrary.Data
{
    public class ReportData : IReportData
    {
        private readonly IConfiguration _config;
        private readonly ISqlDataAccess _db;
        public ReportData(ISqlDataAccess db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        /// <summary>
        /// Funkcija, kas izpilda SQL procedūru spGetReportData
        /// </summary>
        /// <returns>Saraksts ar skrējēja dienasgrāmatas datiem</returns>
        public List<RunnerReportDataModel> GetRunnerReportData(int runnerId, DateTime? dat_s, DateTime? dat_b)
        {
            var p = new DynamicParameters();
                p.Add("RunnerId", runnerId);
                p.Add("Dat_s", dat_s);
                p.Add("Dat_b", dat_b);
            var connstring = "DefaultConnection";
            return _db.LoadDataSP<RunnerReportDataModel, dynamic>("spGetReportData", p, connstring);
        }

        /// <summary>
        /// Funkcija, ļauj skrējējam ierakstīt datus savā dienasgrāmatā. Apstrādā tos pa rindiņām.
        /// Tikai update, jo rindiņas pa datumiem tiek saģenerētas Get funkcijā.
        /// </summary>
        public void UpdateRunnerReportData(RunnerReportDataModel reportdata, int runnerId)
        {
            string myDate = reportdata.Dat.ToString("yyyy-MM-dd");
            string sql = @"UPDATE dbo.RunnerReportData SET planned = @Planned, completed = @Completed, [time] = @Time, km = @Km, pulse = @Pulse, notes = @Notes WHERE dat = @Date AND runner_id = @RunnerId";
            _db.SaveDataSP(sql, new { Planned = reportdata.Planned, Completed = reportdata.Completed, Date = myDate, RunnerId = runnerId, Time = reportdata.Time, Km = reportdata.Km, Pulse = reportdata.Pulse, Notes = reportdata.Notes });

        }
        /// <summary>
        /// Funkcija, kas izveido treniņu ciklu, saģenerē plānoto nedēļu ierakstus un realitātē izpildīto nedēļu ierakstus pēc datumiem.
        /// </summary>
        public List<ReportModel> CreateTrainingCycle(ReportModel trainingCylce)
        {
            var p = new DynamicParameters();
            p.Add("RunnerId", trainingCylce.RunnerId);
            p.Add("Dat_s", trainingCylce.Dat_S);
            p.Add("Dat_b", trainingCylce.Dat_B);
            p.Add("CoachId", trainingCylce.CoachId);
            p.Add("Title", trainingCylce.Title);
            var connstring = "DefaultConnection";
            return _db.LoadDataSP<ReportModel, dynamic>("spCreateTrainingCycle", p, connstring);

        }
        public List<ReportModel> GetTrainingCycles(string id)
        {
            List<ReportModel> a = new List<ReportModel>();
            string sql = @"SELECT runner_id, coach_id, title, dat_s, dat_b FROM dbo.Reports WHERE runner_id = " + id + " ORDER BY dat_s desc"; 
            a = _db.Query<ReportModel>(sql, "DefaultConnection").ToList();
            return a;
        }

        public ReportWeekModel GetCycleWeekCount(int report_id)
        {
            ReportWeekModel week = new ReportWeekModel();
            string connectionString = _config.GetConnectionString("DefaultConnection");
            string sql = @"SELECT dbo.getMaxWeekNum(report_id) week_num_max , week_num from ReportWeek WHERE  GETDATE()>=dat_s and GETDATE() <= dat_b and report_id = " + report_id;
            week = _db.Query<ReportWeekModel>(sql, "DefaultConnection").ToList()[0];
            return week; 
        }
    }



    
}

