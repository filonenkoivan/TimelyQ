using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DataBaseContext
{
    public class AppDbContext : DbContext
    {
        public User Users { get; set; }

        public User Schedule { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
    }
}
