using AutoMapper.Configuration.Conventions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class TestsModel
    {
        [Key]
        public int Id { get; set; }
        public int RunnerId { get; set; }
        public int CoachId { get; set; }
        public DateTime? Dat { get; set; } = DateTime.Now;

        [Range(0.01, 1000.00, ErrorMessage = "Kilometriem jābūt starp 0 un 1000")]
        public float? Km { get; set; }
        public TimeSpan? Pace { get; set; } = TimeSpan.Zero;

    }
}
