using Application.Interfaces.Repository;
using Application.Models.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repository
{
    public class ScheduleRepository(AppDbContext db) : IScheduleRepository
    {
        public async Task AddNewSchedule(ScheduleDTO schedule, int userId)
        {
            UserBusiness? currentUser = await db.UserBusiness.Include(x=>x.Schedule).FirstOrDefaultAsync(x=>x.UserId == userId);

            if(currentUser.Schedule == null)
            {
                Schedule newSchedule = new Schedule()
                {
                    ScheduleEntries = schedule.ScheduleEntries,
                    WorkStartTime = schedule.WorkStartTime,
                    WorkDurationTime = schedule.WorkDurationTime,
                    WorkEndTime = schedule.WorkEndTime,
                };

                currentUser.Schedule = newSchedule;
            }
            else
            {
                currentUser.Schedule.ScheduleEntries = schedule.ScheduleEntries;
                currentUser.Schedule.WorkStartTime = schedule.WorkStartTime;
                currentUser.Schedule.WorkDurationTime = schedule.WorkDurationTime;
                currentUser.Schedule.WorkEndTime = schedule.WorkEndTime;
            }
                await db.SaveChangesAsync();
        }

    }
}
