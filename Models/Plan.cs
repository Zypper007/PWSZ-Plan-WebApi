using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Models
{
    public class Plan : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime? StartSessionDate { get; set; }
        public DateTime? EndSessionDate { get; set; }

        public int YearID { get; set; }
        public virtual Year Year { get; set; }

        public virtual ICollection<PlanClass> Classes { get; set; }
    }
}
