using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class ClassDTO
    {
        public int? SubjectID { get; set; }
        public int? LecurerID { get; set; }
        public int? RoomID { get; set; }
        public TimeSpan? Start { get; set; }
        public TimeSpan? Duration { get; set; }
        public TimeSpan? Recurrence { get; set; }
        public int? TotalHours { get; set; }
    }
}
