using API.Contracts;
using Application.Interfaces;
using Application.Models.DTOs;
using Application.Services;
using System.Security.Claims;

namespace API.Endpoints
{
    public static class ScheduleEndpoints
    {
        public static void MapScheduleEndpoints(this IEndpointRouteBuilder builder)
        {
            builder.MapPost("/create-schedule", CreateNewSchedule);
        }

        public static async Task CreateNewSchedule(ScheduleDTO schedule, ScheduleService scheduleService, HttpContext context)
        {
            var id = int.Parse(context.User.FindFirstValue("UserId"));
            Console.WriteLine(id);
            Console.WriteLine("id is upper");
            await scheduleService.CreateNewSchedule(schedule, id);
        }
    }
}
