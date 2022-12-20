using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class PersonalBestsModel
    {
        [Key]
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public DateTime? Dat { get; set; } = DateTime.Now; 
        public TimeSpan? Time { get; set; } = TimeSpan.Zero;
        [StringLength(50, ErrorMessage = "Nosaukums nedrīkst pārsniegt 50 simbolus")]
        public string? Title { get; set; }
        [StringLength(1000, ErrorMessage = "Apraksta garums pārsniedz atļauto limitu")]
        public string? Description { get; set; }
    }
}
