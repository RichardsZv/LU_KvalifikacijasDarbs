using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class TestsModel
    {
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public int CoachId { get; set; }
        public DateTime? Dat { get; set; }
        public int Km{ get; set; }
        public float Pace { get; set; }

       
    }
}
