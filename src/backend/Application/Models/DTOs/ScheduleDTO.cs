using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DTOs
{
    public class ScheduleDTO
    {
        public List<ScheduleEntry>? ScheduleEntries { get; set; } = new List<ScheduleEntry>();

        public int WorkStartTime { get; set; }
        public int WorkEndTime { get; set; }
        public int WorkDurationTime { get; set; }

        public DateTime LunchTime { get; set; }
        public DateTime TimeForEachClient { get; set; }
    }
}
