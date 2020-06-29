using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class PlanDefaultInformationsDTO
    {
        public int ID { get; set; }
        public InstituteDefaultInformationsDTO Institute { get; set; } 
        public MajorDefaultInformationsDTO Major { get; set; } 
        public SpecializationDefaultInformationsDTO Specialization { get; set; }
        public YearDefaultInformationsDTO Year { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime StartSessionDate { get; set; }
        public DateTime EndSessionDate { get; set; }
        public List<ClassDefaultInformationsDTO> Classes { get; set; }
    }
}
