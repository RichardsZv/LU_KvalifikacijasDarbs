using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Data
{
    public class ReportData
    {
        private readonly ISqlDataAccess _db;
        public ReportData(ISqlDataAccess db)
        {
            _db = db;
        }

        //public List<ReportModel> GetReports()
        //{
        //    string sql = "select * from Atskaite";

        //    return _db.LoadData<ReportModel, dynamic>(sql, new { });
        //}

        //public void InsertReport(ReportModel report)
        //{
        //    string sql = @"insert into dbo.Atskaite (Datums, Ieplanots, Izpildits, Komentars) values (@Datums, @Ieplanots, @Izpildits, @Komentars);";
        //    _db.SaveData(sql, report);
        //}

        //public void UpdateReport(ReportModel report, int Id)
        //{
        //    string sql = @"update dbo.Atskaite set Datums = @Datums, Ieplanots=Ieplanots, Izpildits= @Izpildits, Komentars = @Komentars where Id = " + Id;
        //    _db.SaveData(sql, report);
        //}
    }
}
