using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ReportWeekModel
    {
        public int Id { get; set; }
        public int ReportId{ get; set; }
        public DateTime DatS { get; set; }
        public DateTime DatB { get; set; }
        public int WeekCount { get; set; }
        public int Week_Num { get; set; }
    }
}
