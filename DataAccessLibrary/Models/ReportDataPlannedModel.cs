using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ReportDataPlannedModel
    {
        public int Id { get; set; }
        public ReportWeekModel Week { get; set; }
        public string Description { get; set; }
        public int Weekday { get; set; }
    }
}
