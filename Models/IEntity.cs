using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Models
{
    public interface IEntity
    {
        public int ID { get; set; }
        public DateTime LastUpdate { get; set; }
        public int LastUpdateBy { get; set; }
    }
}
