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
        public Task AddNewSchedule(ScheduleDTO schedule, int userId);
        public Task AddUserToSchedule(int scheduleId, int scheduleEntryId, int userId);
        public Task DeleteUserFromSchedule(int scheduleId, int scheduleEntryId);
        public Task<Schedule> GetSchedule(int scheduleId);
    }
}
