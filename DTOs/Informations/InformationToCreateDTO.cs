using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class InformationToCreateDTO
    {
        [Required]
        public int IDclass { get; set; }
        [Required]
        public int IDuser { get; set; }
        [Required]
        public InformationType Type { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
