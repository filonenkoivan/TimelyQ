using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Models.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ScheduleService(IScheduleRepository repository)
    {
        public async Task CreateNewSchedule(ScheduleDTO schedule, int userId)
        {
            schedule.WorkDurationTime = schedule.WorkEndTime - schedule.WorkStartTime;
            int counter = schedule.WorkStartTime;
            for (var i = 0; i < schedule?.WorkDurationTime; i++)
            {

                schedule?.ScheduleEntries.Add(new ScheduleEntry() { Time = counter});
                counter++;
            }

            await repository.AddNewSchedule(schedule, userId);

        }
        public async Task AddUserToSchedule(int scheduleId, int scheduleEntryId, int userId)
        {
            await repository.AddUserToSchedule(scheduleId, scheduleEntryId, userId);
        }

        public async Task DeleteUserFromSchedule(int scheduleId, int scheduleEntryId)
        {
            await repository.DeleteUserFromSchedule(scheduleId, scheduleEntryId);
        }

        public async Task<Schedule> GetSchedule(int scheduleId)
        {
            return await repository.GetSchedule(scheduleId);
        }
    }
}
