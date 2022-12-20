using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class ReportModel
    {
        [Key]
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public int CoachId { get; set; }
        [StringLength(50, ErrorMessage = "Nosaukums nedrīkst pārsniegt 50 simbolus")]
        public string? Title { get; set; }
        public DateTime? Dat_S { get; set; } = DateTime.Now; 
        public DateTime? Dat_B { get; set; } = DateTime.Now; 

    }

}
