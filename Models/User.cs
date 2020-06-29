using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Models
{
    public class User : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public byte[] HashCode { get; set; }
        public Permission Permission  { get; set; }

        public int? InstituteID { get; set; }
        public virtual Institute? Institute { get; set; }

        public virtual ICollection<Information> Informations { get; set; }

        public virtual ICollection<Class> LecturerClasses { get; set; }
    }
        
    public enum Permission
    {
        NONE = 0,
        LECTURER = 1,
        INSTITUTE = 1 << 1,
        SUPERUSER = 1 << 2,
    }

    
}
