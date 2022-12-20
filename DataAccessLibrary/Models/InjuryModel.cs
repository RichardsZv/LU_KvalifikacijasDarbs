using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class InjuryModel
    {
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public DateTime? Dat { get; set; } = DateTime.Now;

        [StringLength(1000, ErrorMessage = "Apraksta garums pārsniedz atļauto limitu")]
        public string? Description { get; set; }
    }
}
