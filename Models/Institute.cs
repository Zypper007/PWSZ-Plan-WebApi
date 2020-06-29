using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PWSZ_Plan_WebApi.Models
{
    public class Institute : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }

        public virtual ICollection<Major> Majors { get; set; }

        public virtual ICollection<User> Managers { get; set; }
    }
}