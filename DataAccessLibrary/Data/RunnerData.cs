using DataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataAccessLibrary.Data
{
    public class RunnerData : IRunnerData
    {
        private readonly ISqlDataAccess _db;
 
        public RunnerData(ISqlDataAccess db)
        {
            _db = db;

        }

        /// <returns>Šobrīdējo lietotāju</returns>
        public RunnerModel GetCurrentRunner(string uname)
        {
            RunnerModel runner = new RunnerModel();
            string sql = @"SELECT Id, email, birthdate, height, weight, hr, hr_max, gender, start_info, username, firstname, lastname, strava_link from dbo.Users where username = '" + uname+"'";
            //runner = _db.LoadData<RunnerModel, dynamic>(sql, new { })[0];
            var a = _db.Query<RunnerModel>(sql, "DefaultConnection");
            runner = a.FirstOrDefault();
            return runner; 
        }
        /// <summary>
        /// Saglabā info par lietotāju, ja lietotājs jau izveidots, tad update.
        /// </summary>
        public void SaveRunner(RunnerModel runner)
        {
            string sql = "";
            if (runner.Id == 0)
            {
                sql = @"INSERT INTO dbo.Users (email, birthdate, height, weight, hr, hr_max, gender, username, firstname, lastname, strava_link) values (@Email, @Birthdate, @Height, @Weight, @Hr, @HrMax, @Gender, @Username, @Firstname, @Lastname , @StravaLink);";
            }
            else
            {
                sql = @"UPDATE dbo.Users SET email = @Email, birthdate = @Birthdate, height = @Height, weight = @Weight, hr = @Hr, hr_max = @HrMax, gender = @Gender, username = @Username, firstname = @Firstname, lastname = @Lastname, strava_link = @StravaLink where username = @Username";
            }
            _db.SaveDataSP(sql, new { Email = runner.Email, Birthdate = runner.Birthdate, Height = runner.Height, Weight = runner.Weight, Hr = runner.Hr, HrMax = runner.Hr_max, Gender = runner.enumValue, Username = runner.Username, Firstname = runner.Firstname, Lastname = runner.Lastname, StravaLink = runner.Strava_Link });
        }
        /// <summary>
        /// Iegūst visus skrējējus, kas ir aizpildījuši savu informāciju
        /// </summary>
        /// <returns></returns>
        public List<RunnerModel> GetRunners()
        {
            List<RunnerModel> a = new List<RunnerModel>();
            string sql = @"SELECT * from dbo.Users a 
                                left join dbo.CoachRunnerDisplay b on a.Id = b.runner_id
                                where firstname <> '' and lastname <> '' and b.runner_id is null";
            a = _db.Query<RunnerModel>(sql, "DefaultConnection").ToList();
            return a;
        }

        /// <summary>
        /// Pievieno skrējēju trenera audzēkņu sarakstam
        /// </summary>
        /// <param name="Id">Skrējēja Id</param>
        /// <param name="uname">Identity username lai noteiktu šobrīējā lietotāja id</param>
        public void SaveRunnerToCoach(int Id, string uname)
        {
            RunnerModel runner = new RunnerModel();
            string sql = "";
            sql = @"INSERT INTO dbo.CoachRunnerDisplay (runner_id, coach_id) VALUES (@RunnerId, @CoachId)";
            runner = GetCurrentRunner(uname);

            _db.SaveDataSP(sql, new { RunnerId = Id, CoachId = runner.Id});
        }
        /// <summary>
        /// Funkcija treneru skatam
        /// </summary>
        /// <returns>Trenera audzēkņi</returns>
        public List<RunnerModel> GetCoachRunners(string uname)
        {
            RunnerModel runner = new RunnerModel();
            runner = GetCurrentRunner(uname);
            string sql = @"SELECT * FROM dbo.Users a 
                                LEFT JOIN dbo.CoachRunnerDisplay b ON a.Id = b.runner_id
                                WHERE b.coach_id = " + runner.Id; 
            var a = _db.Query<RunnerModel>(sql, "DefaultConnection").ToList();
            return a;
        }
        /// <summary>
        /// Funkcija skrējēja atlasīšanai pēc lietotāja id, priekš skrējēja informācijas attēlošanas trenerim
        /// </summary>
        /// <returns>Trenera audzēkņa info</returns>
        public RunnerModel GetRunnerById(string id)
        {
            RunnerModel runner = new RunnerModel();
            string sql = @"SELECT * FROM dbo.Users WHERE Id =" + id;
            var a = _db.Query<RunnerModel>(sql, "DefaultConnection");
            return a.FirstOrDefault();
        }

        /// <summary>
        /// Funkcija testu pievienošanai audzēknim
        /// </summary>
        public void AddTest(TestsModel test)
        {
            string sql = @"INSERT INTO dbo.Tests (runner_id, coach_id, dat, km, pace) VALUES (@RunnerId, @CoachId, @Dat, @Km, @Pace)";
            _db.SaveDataSP(sql, new { RunnerId = test.RunnerId, CoachId = test.CoachId, Dat = test.Dat, Km = test.Km, Pace = test.Pace });
         
        }

        public List<TestsModel> GetTests(string runner_id)
        {

            string sql = @"SELECT * FROM dbo.Tests 
                                WHERE runner_id = " + runner_id;
            var a = _db.Query<TestsModel>(sql, "DefaultConnection").ToList();
            return a;
        }
   

    }
}
