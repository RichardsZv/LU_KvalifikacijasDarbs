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
using System.Reflection;
using System.Linq.Expressions;

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
        /// Funkcija no datubāzes izsauc pulnu lietotāja treniņu vēsturi. Neizmanto procedūru, jo tā ģenerē datus izvēlētajam laikam. Sacam vienkāršu selectu.  
        /// </summary>
        /// <returns>Saraksts ar skrējēja dienasgrāmatas datiem</returns>
        public List<RunnerReportDataModel> GetRunnerHistoricData(int runnerId)
        {
            List<RunnerReportDataModel> a = new List<RunnerReportDataModel>();
            string sql = @"SELECT * FROM RunnerReportData where runner_id = " + runnerId +" order by dat desc";
            a = _db.Query<RunnerReportDataModel>(sql, "DefaultConnection").ToList();
            return a;
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
        /// <summary>
        /// Funkcija izvēlētā treniņu cikla izdzēšanai
        /// </summary>
        public void DeleteCycle(string runner_id, int cycle_id)
        {
            string sql = @"DELETE FROM Reports WHERE runner_id = " + runner_id + " and Id = " + cycle_id;
            _db.Query<ReportModel>(sql, "DefaultConnection").ToList();
        }

        /// <summary>
        /// Funkcija, kas iegūst audzēkņa treniņu ciklus, treniņu ciklu attēlošanai trenerim
        /// </summary>
        /// <returns>Saraksts ar treniņu cikliem</returns>
        public List<ReportModel> GetTrainingCycles(string id)
        {
            List<ReportModel> a = new List<ReportModel>();
            string sql = @"SELECT id, runner_id, coach_id, title, dat_s, dat_b FROM dbo.Reports WHERE runner_id = " + id + " ORDER BY dat_s desc"; 
            a = _db.Query<ReportModel>(sql, "DefaultConnection").ToList();
            return a;
        }
        /// <summary>
        /// Funkcija, kas iegūst audzēkņa treniņu ciklus, treniņu ciklu attēlošanai trenerim
        /// </summary>
        /// <returns>Saraksts ar treniņu cikliem</returns>
        public ReportModel GetTrainingCycle(string id)
        {
            ReportModel a = new ReportModel();
            string sql = @"SELECT top 1 * FROM dbo.Reports WHERE dat_s <= GETDATE() and dat_b>=GETDATE() and runner_id = " + id;
            a = _db.Query<ReportModel>(sql, "DefaultConnection").ToList()[0];
            return a;         
        }
        /// <summary>
        /// Funkcija, kas iegūst šobrīdējo nedēļu no treniņu cikla
        /// </summary>
        /// <returns>Audzēkņa treniņu nedēļa</returns>
        public ReportWeekModel GetCurrentWeek(string runner_id)
        {
            try
            {
                ReportWeekModel week = new ReportWeekModel();
                string connectionString = _config.GetConnectionString("DefaultConnection");
                string sql = @"SELECT *, dbo.getMaxWeekNum(b.report_id) week_num_max FROM dbo.Reports a inner join ReportWeek b on a.Id = b.report_id WHERE b.dat_s <= CAST(GETDATE() as Date) and b.dat_b >= CAST(GETDATE() as Date) and runner_id = " + runner_id;
                if (sql != null)
                {
                    week = _db.Query<ReportWeekModel>(sql, "DefaultConnection").ToList()[0];
                    return week;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {
                return null; 
            }
            
        }
        /// <summary>
        /// Funkcija padod parametrus procedūrai, kas iegūst datus par plānotajiem treniņiem
        /// </summary>
        /// <returns>Saraksts ar treniņu plānu audzēknim</returns>
        public List<ReportDataPlannedModel> GetPlannedReportList(string runnerId, int weeknum)
        {
            var p = new DynamicParameters();
            p.Add("RunnerId", runnerId);
            p.Add("Week", weeknum);
            var connstring = "DefaultConnection";
            return _db.LoadDataSP<ReportDataPlannedModel, dynamic>("spGetPlannedReportData", p, connstring);
           
        }
        /// <summary>
        /// Ieraksta treniņu plāna izmaiņas datubāzē
        /// </summary>
        public void UpdatePlannedReports(ReportDataPlannedModel reportdata, string runnerId)
        {
            string sql = @"UPDATE ReportDataPlanned  SET plan_description = @Planned WHERE EXISTS( SELECT null FROM ReportWeek b inner join Reports c on c.Id = b.report_id WHERE b.Id = ReportDataPlanned.report_week_id  and c.runner_id = @RunnerId and ReportDataPlanned.dat = @Date)";
            _db.SaveDataSP(sql, new { Planned = reportdata.Plan_Description, Date = reportdata.Dat, RunnerId = runnerId });

        }
        /// <summary>
        /// Iegūst datus tabulai, kur treneris glabā informāciju par audzēkņa izpildītajiem treniņiem
        /// </summary>
        public List<CoachReportDataModel> GetCoachReportList(string runnerId, int weeknum)
        {
            var p = new DynamicParameters();
            p.Add("RunnerId", runnerId);
            p.Add("Week", weeknum);
            var connstring = "DefaultConnection";
            return _db.LoadDataSP<CoachReportDataModel, dynamic>("spGetCoachReportData", p, connstring);
        }
        /// <summary>
        /// Tabulas ieraksta update 
        public void UpdateCoachReport(CoachReportDataModel reportdata, string runnerId)
        {
            string sql = @"UPDATE CoachReportData SET km = @Km
                                , time_run_min = @TimeR
                                , time_other_min = @TimeO
                                , completed = @Completed
                                , pace = @Pace
                                , pulse = @Pulse
                                , vert = @Vert
                                , e = @E
                                , s = @S
                                , r = @R
                                , t = @T
                                , m = @M
                                , vdot = @VDOT
                WHERE EXISTS( 
                    SELECT null FROM ReportWeek b 
                    INNER JOIN Reports c on c.Id = b.report_id 
                    WHERE b.Id = CoachReportData.report_week_id and c.runner_id = @RunnerId and CoachReportData.dat = @Date)";
            _db.SaveDataSP(sql, new
            {
                 Date = reportdata.Dat
                , RunnerId = runnerId
                , Km = reportdata.Km
                , TimeR = reportdata.Time_run_min
                , TimeO = reportdata.Time_other_min
                , Completed = reportdata.Completed
                , Pace = reportdata.Pace
                , Pulse = reportdata.Pulse
                , Vert = reportdata.Vert
                , E = reportdata.E
                , S = reportdata.S
                , R = reportdata.R
                , T = reportdata.T
                , M = reportdata.M
                , VDOT = reportdata.VDOT
            });
        }

        public List<CoachReporDataSumModel> GetSums(string cycle_id)
        {
            var p = new DynamicParameters();
            p.Add("ReportId", cycle_id);
            var connstring = "DefaultConnection";
            return _db.LoadDataSP<CoachReporDataSumModel, dynamic>("spSumByReportWeek", p, connstring);
        }

        public List<CoachReporDataSumModel> GetAverages(string cycle_id)
        {
            var p = new DynamicParameters();
            p.Add("ReportId", cycle_id);
            var connstring = "DefaultConnection";
            return _db.LoadDataSP<CoachReporDataSumModel, dynamic>("spAvgByReportWeek", p, connstring);
        }

    }
}

