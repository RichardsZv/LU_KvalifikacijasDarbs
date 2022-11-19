using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class RunnerModel
    {
        public int Id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string weight { get; set; }
        public string height { get; set; }
        public string max_hr { get; set; }
        public string rest_hr { get; set; }
        public int aspnuserid{ get; set; }
        public bool gender { get; set; }
        public int age { get; set; }
    }
}
