using PWSZ_Plan_WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PWSZ_Plan_WebApi.DTOs
{
    public class ClassDefaultInformationsDTO
    {
        public int ID { get; set; }
        public TimeSpan Start { get; set; }
        public TimeSpan Duration { get; set; }
        public TimeSpan Recurrence { get; set; }
        public int TotalHours { get; set; }
        public SubjectDefaultInformationsDTO Subject { get; set; }
        public UserDefaultInformationsDTO Lecturer { get; set; }
        public RoomDefaultInformationsDTO Room { get; set; }
    }
}
