using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLibrary.Models
{
    public class CoachReporDataSumModel
    {
        //Summas
        public float Km_sum { get; set; }
        public int Time_run_sum { get; set; } //Laiks Minūtēs
        public int Time_other_sum { get; set; } //Laiks Minūtēs   
        public int Vert_sum { get; set; }
        public float E_sum { get; set; }
        public float S_sum { get; set; }
        public float R_sum { get; set; }
        public float I_sum { get; set; }
        public float T_sum { get; set; }
        public float M_sum { get; set; }

        //Vidējās vētības
        public float Km_avg { get; set; }
        public int Time_run_avg { get; set; } //Laiks Minūtēs
        public int Time_other_avg { get; set; } //Laiks Minūtēs   
        public int Vert_avg { get; set; }
        public float E_avg { get; set; }
        public float S_avg { get; set; }
        public float R_avg { get; set; }
        public float I_avg { get; set; }
        public float T_avg { get; set; }
        public float M_avg { get; set; }
    }
}
