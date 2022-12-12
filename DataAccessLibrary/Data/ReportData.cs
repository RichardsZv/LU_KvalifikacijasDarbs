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

namespace DataAccessLibrary.Data
{
    public class ReportData : IReportData
    {
        private readonly ISqlDataAccess _db;
        public ReportData(ISqlDataAccess db)
        {
            _db = db;
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

        public List<ReportModel> CreateTrainingCycle(int runnerId, int coachId, DateTime? dat_s, DateTime? dat_b, ReportModel trainingCycle)
        {
            var p = new DynamicParameters();
                p.Add("RunnerId", runnerId);
                p.Add("Dat_s", dat_s);
                p.Add("Dat_b", dat_b);
                p.Add("CoachId", coachId);
                p.Add("Title", trainingCycle.Title); 
            var connstring = "DefaultConnection";
            return _db.LoadDataSP<ReportModel, dynamic>("spCreateTrainingCycle", p, connstring);

        }

    }



    
}

