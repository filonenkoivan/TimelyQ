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
        public ICollection<ScheduleEntry>? ScheduleEntries { get; set; } = new List<ScheduleEntry>();
        public double WorkStartTime { get; set; }
        public double WorkEndTime { get; set; }
        public double WorkDurationTime { get; set; }
    }

}


