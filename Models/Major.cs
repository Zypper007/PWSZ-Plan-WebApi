using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Models
{
    public class Major : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }

        public int InstituteID { get; set; }
        public virtual Institute Institute { get; set; }

        public virtual ICollection<Specialization> Specializations { get; set; }
    }
}
