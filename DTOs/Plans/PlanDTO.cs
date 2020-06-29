using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class PlanDTO
    {
        public int? YearID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? StartSessionDate { get; set; }
        public DateTime? EndSessionDate { get; set; }
        public List<int>? ClassesID { get; set; }
    }
}
