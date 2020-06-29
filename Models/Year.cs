using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Models
{
    public class Year : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }

        public virtual ICollection<Plan> Plans { get; set; }

        public int SpecializationID { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}
