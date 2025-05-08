using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.DataBaseContext
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserBusiness> UserBusiness { get; set; }
        public DbSet<Schedule> Schedule { get; set; }

        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserBusinessConfiguration());
            modelBuilder.ApplyConfiguration(new ScheduleConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var connectionString = configuration.GetConnectionString("TImelyq");
                optionsBuilder.UseNpgsql(connectionString);
            }
        }
    }
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.UserBusiness)
                .WithOne(s => s.User)
                .HasForeignKey<UserBusiness>(s => s.UserId);
        }
    }
    public class UserBusinessConfiguration : IEntityTypeConfiguration<UserBusiness>
    {
        public void Configure(EntityTypeBuilder<UserBusiness> builder)
        {
            builder.HasKey(x => x.Id);
            builder
                .HasOne(x => x.Schedule)
                .WithOne(s => s.UserBusiness)
                .HasForeignKey<Schedule>(s => s.UserBusinessId);
        }
    }
    public class ScheduleConfiguration : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ScheduleEntries).WithOne(x => x.Schedule).HasForeignKey(x => x.ScheduleId);
        }
    }

}
