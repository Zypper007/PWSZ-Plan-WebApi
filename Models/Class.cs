using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.Models
{
    public class Class : BaseEntity
    {
        public TimeSpan Start { get; set; }
        public TimeSpan Duration{ get; set; }
        public TimeSpan Recurrence { get; set; }
        public int TotalHours { get; set; }

        public virtual ICollection<PlanClass> Plans { get; set; }

        public virtual ICollection<Information> Informations { get; set; }

        public int SubjectID { get; set; }
        public virtual Subject Subject { get; set; }

        public int LecturerID { get; set; }
        public virtual User Lecturer { get; set; }

        public int RoomID { get; set; }
        public virtual Room Room { get; set; }
    }
}
