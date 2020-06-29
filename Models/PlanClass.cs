using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Models
{
    public class PlanClass
    {
        public int PlanID { get; set; }
        public virtual Plan Plan { get; set; }
        public int ClassID { get; set; }
        public virtual Class Class { get; set; }
    }
}
