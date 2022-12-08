using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class InjuryModel
    {
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public DateTime? Dat { get; set; }
        public string Description { get; set; }
    }
}
