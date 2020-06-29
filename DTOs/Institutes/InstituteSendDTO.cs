using AutoMapper;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class InstituteSendDTO : InstituteDefaultInformationsDTO
    {
        public ICollection<MajorDefaultInformationsDTO>? Majors { get; set; }
        public ICollection<UserDefaultInformationsDTO>? Managers { get; set; }
    }
}
