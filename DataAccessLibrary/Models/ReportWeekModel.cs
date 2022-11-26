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
        public DateOnly DatS { get; set; }
        public DateOnly DatB { get; set; }
    }
}
