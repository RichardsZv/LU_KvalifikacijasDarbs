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
        [StringLength(50, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string? Title { get; set; }
        [StringLength(1000, ErrorMessage = "Teksta garums nedrīkst pārsniegt {1} simbolus")]
        public string? Description { get; set; }
    }
}
