﻿using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class UserSendDTO : UserDefaultInformationsDTO
    {
        public InstituteDefaultInformationsDTO? Institute { get; set; }
    }
}
