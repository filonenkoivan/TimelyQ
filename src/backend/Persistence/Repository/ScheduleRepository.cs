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
        public async Task AddNewSchedule(ScheduleDTO schedule, int userId)
        {
            UserBusiness? currentUser = await db.UserBusiness.Include(x=>x.Schedule).FirstOrDefaultAsync(x=>x.UserId == userId);

            if(currentUser?.Schedule == null)
            {
                Schedule newSchedule = new Schedule()
                {
                    ScheduleEntries = schedule.ScheduleEntries,
                    WorkStartTime = schedule.WorkStartTime,
                    WorkDurationTime = schedule.WorkDurationTime,
                    WorkEndTime = schedule.WorkEndTime,
                    LunchTime = schedule.LunchTime,
                    TimeForEachClient = schedule.TimeForEachClient
                };
                currentUser.Schedule = newSchedule;
            }
            else
            {
                currentUser.Schedule.ScheduleEntries = schedule.ScheduleEntries;
                currentUser.Schedule.WorkStartTime = schedule.WorkStartTime;
                currentUser.Schedule.WorkDurationTime = schedule.WorkDurationTime;
                currentUser.Schedule.WorkEndTime = schedule.WorkEndTime;
                currentUser.Schedule.LunchTime = schedule.LunchTime;
                currentUser.Schedule.TimeForEachClient = schedule.TimeForEachClient;
            }
                await db.SaveChangesAsync();
        }

        public async Task AddUserToSchedule(int scheduleId, int scheduleEntryId, int userId)
        {
            var schedule = await db.Schedule.Include(x => x.ScheduleEntries).FirstOrDefaultAsync(x => x.Id == scheduleId);
            var scheduleEntry = schedule?.ScheduleEntries?.FirstOrDefault(x => x.Id == scheduleEntryId);
            var user = await db.Users.FirstOrDefaultAsync(x => x.Id == userId);
            scheduleEntry.Client = user;

            await db.SaveChangesAsync();

            await notifier.NotifyScheduleUpdate(scheduleId);
        }

        public async Task DeleteUserFromSchedule(int scheduleId, int scheduleEntryId)
        {
            var schedule = await db.Schedule.Include(x => x.ScheduleEntries).FirstOrDefaultAsync(x => x.Id == scheduleId);
            var scheduleEntry = schedule?.ScheduleEntries?.FirstOrDefault(x => x.Id == scheduleEntryId);
            if(scheduleEntry == null)
            {
                return;
            }
            scheduleEntry.Client = null;
            scheduleEntry.ClientId = null;

            await db.SaveChangesAsync();

            await notifier.NotifyScheduleUpdate(scheduleId);
        }

        public async Task<Schedule> GetSchedule(int scheduleId)
        {
            return await db.Schedule.FirstOrDefaultAsync(x => x.Id == scheduleId);
        }
    }
}
