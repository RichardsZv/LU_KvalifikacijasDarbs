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

        public List<ReportDataModel> GetReportData(int runnerId, DateTime? dat_s, DateTime? dat_b)
        {
            //Izveidojam dinamiskos parametrus, ko padosim procedūrai
            var p = new DynamicParameters();
            p.Add("RunnerId", runnerId);
            p.Add("Dat_s", dat_s);
            p.Add("Dat_b", dat_b);
            var connstring = "DefaultConnection";
            return _db.LoadDataSP<ReportDataModel, dynamic>("spGetReportData", p, connstring);
        }


        public void UpdateReportData(ReportDataModel reportdata, int runnerId)
        {
            string myDate = reportdata.Dat.ToString("yyyy-MM-dd");
            string sql = @"UPDATE dbo.ReportData SET planned = @Planned, completed = @Completed WHERE dat = @Date AND runner_id = @RunnerId";
            _db.SaveDataSP(sql, new { Planned = reportdata.Planned, Completed = reportdata.Completed, Date = myDate, RunnerId = runnerId });

        }

        public void CreateTrainingCycle(ReportModel reportcycle)
        {
            string sql = @"INSERT INTO dbo.Reports (runner_id, coach_id, title, dat_s, dat_b) VALUES (@RunnerId, @CoachId, @Title, @DatS, @DatB)";
            _db.SaveDataSP(sql, new { RunnerId = reportcycle.RunnerId, CoachId = reportcycle.CoachId, Title = reportcycle.Title, DatS = reportcycle.DatS, DatB = reportcycle.DatB,  });
             
        }

    }



    
}

