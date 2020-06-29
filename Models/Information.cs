using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Models
{
    public class Information : BaseEntity
    {
        public InformationType Type { get; set; }
        [Required]
        [MaxLength(500)]
        public string Message { get; set; }

        public int AuthorID { get; set; }
        public virtual User Author { get; set; }

        public int ClassID { get; set; }
        public virtual Class Class { get; set; }
    }

    public enum InformationType
    {
        Information,
        Absence,
        Exam
    }
}
