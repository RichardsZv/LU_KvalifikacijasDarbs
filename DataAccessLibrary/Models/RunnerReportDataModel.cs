using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class RunnerReportDataModel
    {
        public int Id { get; set; }
        public DateTime Dat { get; set; }
        public string Planned { get; set; }
        public string Completed { get; set; }
        public float Km { get; set; }
        public TimeSpan Time { get; set; }
        public int Pulse { get; set; }
        public string Notes { get; set; }
    }
}
