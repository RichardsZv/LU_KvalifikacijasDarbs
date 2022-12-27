using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccessLibrary.Models.RunnerModel;

namespace DataAccessLibrary.Models
{
    public enum Gender
    {
        Vīrietis,
        Sievietie,
        Cits,
    }

    public class RunnerModel
    {
        [Key]
        public int Id { get; set; }
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } 
        public DateTime? Birthdate { get; set; }
        public int Height { get; set; } 
        public int Weight { get; set; } 
        public int Hr{ get; set; } 
        public int Hr_max { get; set; }
        public Gender gender { get; set; }
        public string Start_info { get; set; }
        public string Username{ get; set; }
        //public string AspUserId{ get; set; }    
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Strava_Link { get; set; }
        public string Garmin_link { get; set; }
    }
   

}
