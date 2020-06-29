﻿using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class SpecializationSendDTO : SpecializationDefaultInformationsDTO
    {
        public MajorDefaultInformationsDTO Major { get; set; }
        public ICollection<YearDefaultInformationsDTO>? Years { get; set; }
    }
}
