using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class CoachReportDataModel
    {
        public int Id { get; set; }
        public float Km { get; set; }
        public int Time { get; set; } //Laiks Minūtēs
        public string Completed { get; set; }
        public TimeSpan Pace { get; set; }
        public int Pulse { get; set; }
        public int Vert { get; set; }
        public float E { get; set; }
        public float S { get; set; }
        public float R { get; set; }
        public float I { get; set; }
        public float T { get; set; }
        public float M { get; set; }
        public float VDOT { get; set; }
     
    }
}
