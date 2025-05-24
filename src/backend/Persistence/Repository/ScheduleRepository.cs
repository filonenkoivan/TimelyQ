using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Models.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ScheduleRepository(AppDbContext db, IDashboardNotifier notifier) : IScheduleRepository
    {
        public async Task AddNewSchedule(Schedule schedule, int userId)
        {
            UserBusiness? currentUser = await db.UserBusiness.Include(x=>x.Schedule).FirstOrDefaultAsync(x=>x.UserId == userId);
            currentUser.Schedule = schedule;
            await db.SaveChangesAsync();
        }

        public async Task<bool> AddUserToSchedule(int scheduleId, int scheduleEntryId, int userId)
        {
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            var schedule = await db.Schedule.Include(x => x.ScheduleEntries).FirstOrDefaultAsync(x => x.Id == scheduleId);
            var scheduleEntry = schedule?.ScheduleEntries?.FirstOrDefault(x => x.Id == scheduleEntryId);

            if (user == null)
                return false;

            if (schedule == null)
                return false;

            user?.Entries?.Add(scheduleEntry);
            scheduleEntry.Client = user;

            await db.SaveChangesAsync();

            await notifier.NotifyScheduleUpdate(scheduleId);

            return true;
        }

        public async Task<bool> DeleteUserFromSchedule(int scheduleId, int scheduleEntryId, int userId)
        {
            var schedule = await db.Schedule.Include(x => x.ScheduleEntries).FirstOrDefaultAsync(x => x.Id == scheduleId);
            var scheduleEntry = schedule?.ScheduleEntries?.FirstOrDefault(x => x.Id == scheduleEntryId);
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (scheduleEntry == null)
            {
                return false;
            }
            user?.Entries?.Remove(scheduleEntry);
            scheduleEntry.Client = null;
            scheduleEntry.ClientId = null;

            await db.SaveChangesAsync();

            await notifier.NotifyScheduleUpdate(scheduleId);

            return true;
        }

        public async Task<Schedule?> GetSchedule(int scheduleId)
        {
            var schedule = db.Schedule.Include(x => x.ScheduleEntries);
            

            return await schedule.FirstOrDefaultAsync(x => x.Id == scheduleId);
        }

        public async Task<bool> AddTimeIntervalToSchedule(int scheduleId, double startInterval, double endInterval, bool isLunch)
        {
            var schedule = await db.Schedule.Include(x=>x.ScheduleEntries).FirstOrDefaultAsync(x => x.Id == scheduleId);

            if(isLunch == true)
            {
                schedule?.ScheduleEntries?.Add(new ScheduleEntry()
                {
                    StartTime = TimeSpan.FromMinutes(startInterval),
                    EndTime = TimeSpan.FromMinutes(endInterval),
                    IsLunch = true
                    
                });
                await db.SaveChangesAsync();
                return false;
            }
            DateTime currentScheduleTime = schedule.CreatedTime.Date;

            var scheduleEntry = new ScheduleEntry
            {
                StartTime = TimeSpan.FromMinutes(startInterval),
                EndTime = TimeSpan.FromMinutes(endInterval),
            };
            scheduleEntry.DateTime = currentScheduleTime.Add(TimeSpan.FromMinutes(startInterval));

            schedule?.ScheduleEntries?.Add(scheduleEntry);
            await db.SaveChangesAsync();

            await notifier.NotifyScheduleUpdate(scheduleId);

            return true;
        }
    }
}
