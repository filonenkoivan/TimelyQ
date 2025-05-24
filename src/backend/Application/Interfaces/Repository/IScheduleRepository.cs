using Application.Models.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repository
{
    public interface IScheduleRepository
    {
        public Task AddNewSchedule(Schedule schedule, int userId);
        public Task<bool> AddUserToSchedule(int scheduleId, int scheduleEntryId, int userId);
        public Task<bool> DeleteUserFromSchedule(int scheduleId, int scheduleEntryId, int userId);
        public Task<Schedule?> GetSchedule(int scheduleId);
        public Task<bool> AddTimeIntervalToSchedule(int scheduleId, double startInterval, double endInterval, bool isLunch);
    }
}
