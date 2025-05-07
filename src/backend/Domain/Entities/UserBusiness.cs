using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Admin : User
    {
        public string? CompanyName { get; set; }
        public CompanyCategory CompanyCategory { get; set; }
        public Schedule? Schedule { get; set; }

    }
}
