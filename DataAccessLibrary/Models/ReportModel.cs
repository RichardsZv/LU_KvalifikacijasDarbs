using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ReportModel
    {
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public int CoachId { get; set; }
        public string Title { get; set; }
        public DateTime? Dat_S { get; set; } = DateTime.Now; 
        public DateTime? Dat_B { get; set; } = DateTime.Now; 

    }

}
