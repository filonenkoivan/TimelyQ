using Application.Enums;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Models;
using Application.Models.DTOs;
using Domain.Entities;
using Microsoft.Extensions.Logging;
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
        //ADMIN
        public async Task CreateNewSchedule(ScheduleDTO scheduleDto, int userId)
        {
            scheduleDto.WorkDurationTime = scheduleDto.WorkEndTime - scheduleDto.WorkStartTime;

            var schedule = new Schedule
            {
                WorkStartTime = TimeSpan.FromHours(scheduleDto.WorkStartTime),
                WorkEndTime = TimeSpan.FromHours(scheduleDto.WorkEndTime),
                WorkDurationTime = TimeSpan.FromHours(Math.Abs(scheduleDto.WorkEndTime - scheduleDto.WorkStartTime)),
                CreatedTime = DateTime.UtcNow
            };

            await repository.AddNewSchedule(schedule, userId);

        }
        public async Task<bool> AddTimeIntervalToSchedule(int scheduleId, double startInterval, double endInterval, bool isLunch = false)
        {
            var schedule = await repository.GetSchedule(scheduleId);

            if(schedule == null)
            {
                return false;
            }
            if ((schedule?.ScheduleEntries?.Count == 0 ||
                schedule.ScheduleEntries.All(
                    x => x.StartTime.TotalMinutes >= endInterval || x.EndTime.TotalMinutes <= startInterval))
                && startInterval < endInterval)
            {
                await repository.AddTimeIntervalToSchedule(scheduleId, startInterval, endInterval, isLunch);
                return true;
            }

            return false;
        }
        public async Task<bool> DeleteUserFromSchedule(int scheduleId, int scheduleEntryId, int userId)
        {
            return await repository.DeleteUserFromSchedule(scheduleId, scheduleEntryId, userId);
        }
        public async Task<Schedule?> GetSchedule(int scheduleId)
        {
            return await repository.GetSchedule(scheduleId);
        }
        //CUSTOMER
        public async Task<bool> AddUserToSchedule(int scheduleId, int scheduleEntryId, int userId)
        {
            return await repository.AddUserToSchedule(scheduleId, scheduleEntryId, userId);
        }
    }
}
