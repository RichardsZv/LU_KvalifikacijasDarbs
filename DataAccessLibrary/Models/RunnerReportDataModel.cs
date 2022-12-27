using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class RunnerReportDataModel
    {
        public int Id { get; set; }
        public DateTime Dat { get; set; }
        public string? Planned { get; set; }
        public string? Completed { get; set; }

        [Range(0.01, 1000.00, ErrorMessage = "Vērtībai jābūt starp {1} un {2}")]
        public float Km { get; set; }
        public TimeSpan Time { get; set; }
        public int Pulse { get; set; }
        [StringLength(1000, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string? Notes { get; set; }
    }
}
