﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Login { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public Roles Role { get; set; }
        public int Id { get; set; }

        public ICollection<ScheduleEntry>? Entries { get; set; } = new List<ScheduleEntry>();
        public UserBusiness? UserBusiness { get; set; }
    }
}
