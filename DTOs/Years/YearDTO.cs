﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class YearDTO
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int? SpecializationID { get; set; }
    }
}
