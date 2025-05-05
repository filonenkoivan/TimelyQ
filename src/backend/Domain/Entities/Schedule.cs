using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Schedule
    {
        public List<User>? UserList { get; set; }

        public TimeSpan LunchTime { get; set; }

        public TimeSpan TimeForEachClient { get; set; }

        public int Id { get; set; }
        public int AdminId { get; set; }
    }
}
