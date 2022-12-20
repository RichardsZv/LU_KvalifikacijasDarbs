using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class RaceModel
    {
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public DateTime? Dat { get; set; } = DateTime.Now;
        [StringLength(50, ErrorMessage ="Nosaukums nedrīkst pārsniegt 50 simbolus")]
        public string Title { get; set; }
        public TimeSpan? Result { get; set; } = TimeSpan.Zero;
        [StringLength(10, ErrorMessage = "Iegūtās vietas raksturojums nedrīkst pārsniegt 10 simbolus")]
        public string Place { get; set; }

        [StringLength(10, ErrorMessage = "Grupas raksturojums nedrīkst pārsniegt 10 simbolus")]
        public string Group { get; set; }

        [StringLength(1000, ErrorMessage = "Pierakstu garums pārsniedz atļauto limitu")]
        public string Notes { get; set; }
        [Range(0, 5, ErrorMessage = "Vērtējumam jābūt 1-5")]
        public int Evaluation { get; set; }
    }
}
