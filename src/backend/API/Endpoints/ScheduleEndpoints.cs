using API.Contracts;
using Application.Interfaces;
using Application.Models.DTOs;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Endpoints
{
    public static class ScheduleEndpoints
    {
        public static void MapScheduleEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("schedule/create-schedule", CreateNewSchedule);
            builder.MapPost("schedule/add-user", AddUserToSchedule);
            builder.MapDelete("schedule/{scheduleId}/delete-user/{scheduleEntryId}", DeleteUserFromSchedule);
        }

        [Authorize(Roles = "UserBusiness")]
        public static async Task CreateNewSchedule(ScheduleDTO schedule, ScheduleService scheduleService, HttpContext context)
        {
            Console.WriteLine(context.User.IsInRole("UserBusiness"));

            var id = int.Parse(context.User.FindFirstValue("UserId"));
            await scheduleService.CreateNewSchedule(schedule, id);
        }
        [Authorize]
        public static async Task AddUserToSchedule(UserToScheduleControll userToScheduleControll, ScheduleService scheduleService, HttpContext context)
        {
            var id = int.Parse(context.User.FindFirstValue("UserId"));
            await scheduleService.AddUserToSchedule(userToScheduleControll.scheduleId, userToScheduleControll.scheduleEntryId, id);
        }
        [Authorize]
        public static async Task DeleteUserFromSchedule(int scheduleId, int scheduleEntryId,ScheduleService scheduleService)
        {
            await scheduleService.DeleteUserFromSchedule(scheduleId, scheduleEntryId);
        }

        //public static async Task UpdateUserInSchedule(int scheduleId)
    }
}
