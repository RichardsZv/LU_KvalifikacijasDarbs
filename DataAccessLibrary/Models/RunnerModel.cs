using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
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
        [Range(0, 1000, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public int Height { get; set; }
        [Range(0, 1000, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public int Weight { get; set; }
        [Range(0, 1000, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public int Hr{ get; set; }
        [Range(0, 1000, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public int Hr_max { get; set; }
        public Gender gender { get; set; }
        [StringLength(10000, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string? Start_info { get; set; }
        public string? Username{ get; set; }
        [StringLength(50, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string? Firstname { get; set; }
        [StringLength(50, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string? Lastname { get; set; }
        [StringLength(250, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string? Strava_Link { get; set; }
        [StringLength(250, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string? Garmin_link { get; set; }

        public int Status { get; set; }
    }
   

}
