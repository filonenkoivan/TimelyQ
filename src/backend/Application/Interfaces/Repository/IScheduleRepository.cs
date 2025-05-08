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
    }
}
