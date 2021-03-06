﻿using PWSZ_Plan_WebApi.DTOs;
using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Data
{
    public interface IUserRepository : IGenericRepository<User>
    {
        void Test();
        Task<User> Auth(string Code);
    }
}
