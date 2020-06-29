using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class MajorSendDTO : MajorDefaultInformationsDTO
    {
        public InstituteDefaultInformationsDTO Institute { get; set; }
        public ICollection<SpecializationDefaultInformationsDTO> Specializations { get; set; }
    }
}
