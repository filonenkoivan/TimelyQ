using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Schedule
    {
        public int Id { get; set; }
        public ICollection<ScheduleEntry>? ScheduleEntries { get; set; } = new List<ScheduleEntry>();

        public TimeSpan WorkStartTime { get; set; }
        public TimeSpan WorkEndTime { get; set; }
        public TimeSpan WorkDurationTime { get; set; }


        [JsonIgnore]
        public UserBusiness? UserBusiness { get; set; }
        public int UserBusinessId { get; set; }

        public DateTime CreatedTime { get; set; }

    } 
}
