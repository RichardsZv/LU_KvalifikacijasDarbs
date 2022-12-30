using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class CoachReportDataModel
    {
        public int Id { get; set; }
        [Range(0.01, 1000.00, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public float Km { get; set; }
        [Range(0, 10000, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public int Time_run_min { get; set; } //Laiks Minūtēs

        [Range(0, 10000, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public int Time_other_min { get; set; } //Laiks Minūtēs
        [StringLength(250, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string? Completed { get; set; }
        public TimeSpan? Pace { get; set; } = TimeSpan.Zero;
        [Range(0, 500, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public int Pulse { get; set; }
        [Range(0, 10000, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public int Vert { get; set; }
        public float E { get; set; }
        public float S { get; set; }
        public float R { get; set; }
        public float I { get; set; }
        public float T { get; set; }
        public float M { get; set; }
        public int VDOT { get; set; } 
        public DateTime Dat { get; set; }
     
    }
}
