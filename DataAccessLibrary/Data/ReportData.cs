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

        public RunnerModel GetReport(string uname)
        {


            //runner = _db.LoadData<RunnerModel, dynamic>(sql, new { })[0];
            //var a = _db.Query<ReportModel>(sql, "DefaultConnection");
            return null; 
        }
    }
}
