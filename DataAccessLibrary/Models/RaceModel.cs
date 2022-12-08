using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class RaceModel
    {
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public DateTime? Dat { get; set; }
        public string Title { get; set; }
        public TimeSpan? Result { get; set; }
        public string Place { get; set; }
        public string Group { get; set; }
        public string Notes { get; set; }
        public int Evaluation { get; set; }
    }
}
