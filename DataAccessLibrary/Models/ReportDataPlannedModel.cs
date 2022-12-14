using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ReportDataPlannedModel
    {
        [Key]
        public int Id { get; set; }
        public DateTime Dat { get; set; }
        public int Report_Week_Id { get; set; }
        [StringLength(150, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string Plan_Description { get; set; }
    }
}
