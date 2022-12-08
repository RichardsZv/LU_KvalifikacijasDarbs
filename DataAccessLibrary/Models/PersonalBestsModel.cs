using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class PersonalBestsModel
    {
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public DateTime? Dat { get; set; } = DateTime.Now; 
        public TimeSpan? Time { get; set; } = TimeSpan.Zero;
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
