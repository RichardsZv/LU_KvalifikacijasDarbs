using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ReportDataModel
    {
        public int Id { get; set; }
        //Iespējams jāmaina uz public ReportWeek reportweek {get; set;}
        public DateTime Dat { get; set; } 
        public float Km { get; set; }
        public int Hr { get; set; }
        public int TimeRun { get; set; }
        public int TimeOther { get; set; }
        public int Vert { get; set; }
        public float E { get; set; }
        public float S { get; set; }
        public float R { get; set; }
        public float I { get; set; }
        public float T { get; set; }
        public float M { get; set; }
        public float VDOT { get; set; }
        public string Planned { get; set; }
        public string Completed { get; set;  }
        public string Notes { get; set; }
    }
}
