using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ScheduleEntry
    {
        public int Id { get; set; }
        [JsonIgnore]
        public User? Client { get; set; }
        public int Time { get; set; }
        [JsonIgnore]
        public Schedule? Schedule { get; set; }
        public int ScheduleId { get; set; }
    }
}
