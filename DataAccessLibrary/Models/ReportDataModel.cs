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
        public int ReportWeekId { get; set; }
        public int WeekDay { get; set; }
        public float Km { get; set; }
        public int Time { get; set; }
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
