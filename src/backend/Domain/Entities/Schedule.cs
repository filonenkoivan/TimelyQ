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
        public List<ScheduleEntry>? ScheduleEntries { get; set; } = new List<ScheduleEntry>();
        public int WorkStartTime { get; set; }
        public int WorkEndTime { get; set; }
        public int WorkDurationTime { get; set; }

        public int LunchTime { get; set; }
        public int TimeForEachClient { get; set; } 

        [JsonIgnore]
        public UserBusiness? UserBusiness { get; set; }
        public int UserBusinessId { get; set; }

    } 
}
