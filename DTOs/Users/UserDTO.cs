using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class UserDTO
    {
        public string? Name { get; set; }
        public string? Code { get; set; }
        public Permission? Permission { get; set; }
        public bool? IsLecturer { get; set; }
        public int? InstituteID { get; set; }
    }
}
