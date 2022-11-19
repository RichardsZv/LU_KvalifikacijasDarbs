using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Data
{
    public class RunnerData
    {
        private readonly ISqlDataAccess _db;
        public RunnerData(ISqlDataAccess db)
        {
            _db = db;
        }

        public RunnerModel GetCurrentRunner()
        {
            int UID = 1; 
            string sql = @"select * from dbo.Skrejeji where aspuserid = " + UID;

            var a = _db.LoadData<RunnerModel, dynamic>(sql, new { });
            return a[0]; 
        }


    }
}
