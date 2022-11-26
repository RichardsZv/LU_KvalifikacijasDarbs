using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Data
{
    public class RunnerData : IRunnerData
    {
        private readonly ISqlDataAccess _db;
 
        public RunnerData(ISqlDataAccess db)
        {
            _db = db;

        }

        public RunnerModel GetCurrentRunner(string uname)
        {
            RunnerModel runner = new RunnerModel();
            string sql = @"SELECT Id, email, birthdate, height, weight, hr, hr_max, gender, start_info, username, firstname, lastname from dbo.Users where username = '"+uname+"'";

            //runner = _db.LoadData<RunnerModel, dynamic>(sql, new { })[0];
            var a = _db.Query<RunnerModel>(sql, "DefaultConnection");
            runner = a.FirstOrDefault();
            return runner; 
        }

        public void SaveRunner(RunnerModel runner)
        {
            string sql = "";
            if (runner.Id == 0)
            {
                sql = @"INSERT INTO dbo.Users (email, birthdate, height, weight, hr, hr_max, gender, username, firstname, lastname) values (@Email, @Birthdate, @Height, @Weight, @Hr, @HrMax, @Gender, @Username, @Firstname, @Lastname );";
            }
            else
            {
                sql = @"UPDATE dbo.Users SET email = @Email, birthdate = @Birthdate, height = @Height, weight = @Weight, hr = @Hr, hr_max = @HrMax, gender = @Gender, username = @Username, firstname = @Firstname, lastname = @Lastname where username = @Username";
            }

            _db.SaveDataSP(sql, new { Email = runner.Email, Birthdate = runner.Birthdate, Height = runner.Height, Weight = runner.Weight, Hr = runner.Hr, HrMax = runner.Hr_max, Gender = runner.enumValue, Username = runner.Username, Firstname = runner.Firstname, Lastname = runner.Lastname });


        }

    //    public void CreateUser(IIdentity i)
    //    {
    //        string sql = "";
    //        sql = @"INSERT INTO dbo.Users (email, username, aspid) values (@Email, @Email, @ApsId);";
    //        _db.SaveDataSP(sql, new { Email = runner.Email, Birthdate = runner.Birthdate, Height = runner.Height, Weight = runner.Weight, Hr = runner.Hr, HrMax = runner.Hr_max, Gender = runner.enumValue, Username = runner.Username, Firstname = runner.Firstname, Lastname = runner.Lastname });

    //    }
    }
}
