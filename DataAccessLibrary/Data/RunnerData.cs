using Dapper;
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
            try
            {
                RunnerModel runner = new RunnerModel();
                string sql = @"SELECT Id, email, birthdate, height, weight, hr, hr_max, gender, start_info, username, firstname, lastname, strava_link from dbo.Users where username = '" + uname + "'";
                //runner = _db.LoadData<RunnerModel, dynamic>(sql, new { })[0];
                var a = _db.Query<RunnerModel>(sql, "DefaultConnection");
                runner = a.FirstOrDefault();
                return runner;
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }
        /// <summary>
        /// Saglabā info par lietotāju, ja lietotājs jau izveidots, tad update.
        /// </summary>
        public void SaveRunner(RunnerModel runner)
        {
            try
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
                _db.SaveDataSP(sql, new { Email = runner.Email, Birthdate = runner.Birthdate, Height = runner.Height, Weight = runner.Weight, Hr = runner.Hr, HrMax = runner.Hr_max, Gender = runner.gender, Username = runner.Username, Firstname = runner.Firstname, Lastname = runner.Lastname, StravaLink = runner.Strava_Link });
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }
        /// <summary>
        /// Iegūst visus skrējējus, kas ir aizpildījuši savu informāciju
        /// </summary>
        /// <returns>Saraksts ar ar skrējējiem</returns>
        public List<RunnerModel> GetRunners()
        {
            try
            {
                List<RunnerModel> a = new List<RunnerModel>();
                string sql = @"SELECT * from dbo.Users a 
                                left join dbo.CoachRunnerDisplay b on a.Id = b.runner_id
                                where firstname <> '' and lastname <> '' and b.runner_id is null";
                a = _db.Query<RunnerModel>(sql, "DefaultConnection").ToList();
                return a;
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }

        /// <summary>
        /// Pievieno skrējēju trenera audzēkņu sarakstam
        /// </summary>
        /// <param name="Id">Skrējēja Id</param>
        /// <param name="uname">Identity username lai noteiktu šobrīējā lietotāja id</param>
        public void SaveRunnerToCoach(int Id, string uname)
        {
            try
            {
                RunnerModel runner = new RunnerModel();
            string sql = "";
            sql = @"INSERT INTO dbo.CoachRunnerDisplay (runner_id, coach_id) VALUES (@RunnerId, @CoachId)";
            runner = GetCurrentRunner(uname);

            _db.SaveDataSP(sql, new { RunnerId = Id, CoachId = runner.Id});
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }
            
        /// <summary>
        /// Funkcija treneru skatam
        /// </summary>
        /// <returns>Trenera audzēkņi</returns>
        public List<RunnerModel> GetCoachRunners(int id )
        {
            try
            {
                string sql = @"SELECT * FROM dbo.Users a 
                                LEFT JOIN dbo.CoachRunnerDisplay b ON a.Id = b.runner_id
                                WHERE b.coach_id = " + id;
                var a = _db.Query<RunnerModel>(sql, "DefaultConnection").ToList();
                return a;
            }
            catch(Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }
        /// <summary>
        /// Funkcija skrējēja atlasīšanai pēc lietotāja id, priekš skrējēja informācijas attēlošanas trenerim
        /// </summary>
        /// <returns>Trenera audzēkņa info</returns>
        public RunnerModel GetRunnerById(string id)
        {
            try
            {
                RunnerModel runner = new RunnerModel();
                string sql = @"SELECT * FROM dbo.Users WHERE Id =" + id;
                var a = _db.Query<RunnerModel>(sql, "DefaultConnection").ToList()[0];
                return a;
            }
            catch(Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
            
        }

        /// <summary>
        /// Funkcija testu pievienošanai audzēknim
        /// </summary>
        public void AddTest(TestsModel test)
        {
            try
            {
                string sql = @"INSERT INTO dbo.Tests (runner_id, coach_id, dat, km, pace) VALUES (@RunnerId, @CoachId, @Dat, @Km, @Pace)";
                _db.SaveDataSP(sql, new { RunnerId = test.RunnerId, CoachId = test.CoachId, Dat = test.Dat, Km = test.Km, Pace = test.Pace });
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }

        }
        /// <summary>
        /// Funkcija audzēķņa testu iegūšanai
        /// </summary>
        public List<TestsModel> GetTests(string runner_id)
        {
            try
            {
                string sql = @"SELECT * FROM dbo.Tests 
                                WHERE runner_id = " + runner_id;
                var a = _db.Query<TestsModel>(sql, "DefaultConnection").ToList();
                return a;
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }
        /// <summary>
        /// Funkcija izvēlētā testa izdēšānai
        /// </summary>
        public void DeleteTest(string runner_id, int test_Id)
        {
            try
            {
                string sql = @"DELETE FROM Tests WHERE runner_id = " + runner_id + " and Id = " + test_Id;
                _db.Query<TestsModel>(sql, "DefaultConnection").ToList();

            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }

        }


        /// <summary>
        /// Funkcija sacensību pievienošanai audzēknim
        /// </summary>
        public void AddRace(RaceModel race)
        {
            try
            {
                string sql = @"INSERT INTO dbo.Races (runner_id, dat, title, result, place, [group], notes, evaluation) VALUES (@RunnerId, @Dat, @Title, @Result, @Place, @Group, @Notes, @Evaluation)";
                _db.SaveDataSP(sql, new { RunnerId = race.RunnerId, Dat = race.Dat, Title = race.Title, Result = race.Result, Place = race.Place, Group = race.Group, Notes = race.Notes, Evaluation = race.Evaluation });
            }
            catch(Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }

        }
        /// <summary>
        /// Funkcija izvēlētās sacīkstes izdzēšanai
        /// </summary>
        public void DeleteRace(string runner_id, int race_id)
        {
            try
            {
                string sql = @"DELETE FROM Races WHERE runner_id = " + runner_id + " and Id = " + race_id;
                _db.Query<RaceModel>(sql, "DefaultConnection").ToList();
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }

        /// <summary>
        /// Funkcija audzēķņa sacensību iegūšanai
        /// </summary>
        public List<RaceModel> GetRaces(string runner_id)
        {
            try
            {
                string sql = @"SELECT * FROM dbo.Races
                                WHERE runner_id = " + runner_id;
                var a = _db.Query<RaceModel>(sql, "DefaultConnection").ToList();
                return a;
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }

        /// <summary>
        /// Funkcija audzēkņa traumu pievienošanai
        /// </summary>
        public void AddInjury(InjuryModel injury)
        {
            try
            {
                string sql = @"INSERT INTO dbo.Injuries (runner_id, dat, description) VALUES (@RunnerId, @Dat, @Description)";
                _db.SaveDataSP(sql, new { RunnerId = injury.RunnerId, Dat = injury.Dat, Description = injury.Description });
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }

        }
        /// <summary>
        /// Funkcija izvēlētās sacīkstes izdzēšanai
        /// </summary>
        public void DeleteInjury(string runner_id, int pb_id)
        {
            try
            {
                string sql = @"DELETE FROM Injuries WHERE runner_id = " + runner_id + " and Id = " + pb_id;
                _db.Query<InjuryModel>(sql, "DefaultConnection").ToList();
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }

        /// <summary>
        /// Funkcija audzēķņa traumu iegūšanai
        /// </summary>
        public List<InjuryModel> GetInjuries(string runner_id)
        {
            try
            {
                string sql = @"SELECT * FROM dbo.Injuries
                                WHERE runner_id = " + runner_id;
                var a = _db.Query<InjuryModel>(sql, "DefaultConnection").ToList();
                return a;
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }
        /// <summary>
        /// Funkcija audzēkņa personīgo rekordu pievienošanai
        /// </summary>
        public void AddPersonalBest(PersonalBestsModel pb)
        {
            try
            {
                string sql = @"INSERT INTO dbo.Records (runner_id, dat, time, title, description) VALUES (@RunnerId, @Dat, @Time, @Title, @Description)";
                _db.SaveDataSP(sql, new { RunnerId = pb.RunnerId, Dat = pb.Dat, Time = pb.Time, Title = pb.Title, Description = pb.Description });
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }
        /// <summary>
        /// Funkcija izvēlētā personīgā rekorda izdēšanai
        /// </summary>
        public void DeletePb(string runner_id, int pb_id)
        {
            try
            {
                string sql = @"DELETE FROM Records WHERE runner_id = " + runner_id + " and Id = " + pb_id;
                _db.Query<PersonalBestsModel>(sql, "DefaultConnection").ToList();
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }

        /// <summary>
        /// Funkcija personīgo rekordu iegūšanai
        /// </summary>
        public List<PersonalBestsModel> GetPersonalBests(string runner_id)
        {
            try
            {
                string sql = @"SELECT * FROM dbo.Records
                                WHERE runner_id = " + runner_id;
                var a = _db.Query<PersonalBestsModel>(sql, "DefaultConnection").ToList();
                return a;
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }

        /// <summary>
        /// Funkcija sistēmas lietotāja dzēšanai no trenera audzēkņu sarakstsa
        /// </summary>
        public void RemoveTrainee(int runner_id, int coach_id)
        {
            try
            {
                string sql = @"DELETE FROM CoachRunnerDisplay WHERE runner_id = " + runner_id + " and coach_id = " + coach_id;
                _db.Query<RunnerModel>(sql, "DefaultConnection").ToList();
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }

        /// <summary>
        /// Funkcija, kas iegūst audzekņa atskaites statusu balstoties uz dienasgrāmatas aizpildījuma datiem. 
        /// </summary>
        public int GetStatus(int runnerId)
        {
            try
            {
                var p = new DynamicParameters();
                p.Add("RunnerId", runnerId);
                var connstring = "DefaultConnection";
                var a = _db.LoadDataSP<RunnerModel, dynamic>("spGetReportStatus", p, connstring).FirstOrDefault();
                int i = a.Status;
                return i;
            }
            catch (Exception ex) { throw new ArgumentException(ex.Message, ex.InnerException); }
        }


    }
}
