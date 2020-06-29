using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class UserDefaultInformationsDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Permission Permission { get; set; }
    }
}
