using API.Contracts;
using Application.Enums;
using Application.Interfaces;
using Application.Models;
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
            builder.MapPost("schedule/{scheduleId}/add-interval", AddTimeIntervalToSchedule);
            builder.MapPost("schedule/add-user", AddUserToSchedule);
            builder.MapDelete("schedule/{scheduleId}/delete-user/{scheduleEntryId}", DeleteUserFromSchedule);

        }
        //Endoints for admins
        [Authorize(Roles = "UserBusiness")]
        public static async Task CreateNewSchedule(
            ScheduleContract schedule,
            ScheduleService scheduleService,
            HttpContext context)
        {
            var id = int.Parse(context.User.FindFirstValue("UserId"));
            var scheduleDto = new ScheduleDTO
            {
                WorkEndTime = schedule.WorkStartTime,
                WorkStartTime = schedule.WorkEndTime
            };
            await scheduleService.CreateNewSchedule(scheduleDto, id);
        }
        [Authorize(Roles = "UserBusiness")]
        public static async Task<IResult> AddTimeIntervalToSchedule(
            TimeInterval timeInterval,
            HttpContext context,
            ScheduleService scheduleService,
            int scheduleId,
            bool isLunch)
        {
            var result = await scheduleService.AddTimeIntervalToSchedule(scheduleId, timeInterval.StartInterval, timeInterval.EndInterval, isLunch);

            if (result)
            {
                return Results.Ok(new BasicResponse<string>(StatusCode.Success, "Time interval added"));
            }
            else
            {
                return Results.Ok(new BasicResponse<string>(StatusCode.BadRequest, "Invalid time interval, time should not overlap"));
            }

        }
        //Endoints for customers
        [Authorize]
        public static async Task<IResult> AddUserToSchedule(
            UserToScheduleControll userToScheduleControll,
            ScheduleService scheduleService,
            HttpContext context)
        {
            var id = int.Parse(context.User.FindFirstValue("UserId"));
            var result = await scheduleService.AddUserToSchedule(userToScheduleControll.scheduleId, userToScheduleControll.scheduleEntryId, id);

            if (result)
            {
                return Results.Ok(new BasicResponse<string>(StatusCode.Success, "New user added to the schedule"));
            }
            else
            {
                return Results.Ok(new BasicResponse<string>(StatusCode.BadRequest, "User already exists in the schedule"));
            }
        }
        [Authorize]
        public static async Task<IResult> DeleteUserFromSchedule(
            int scheduleId,
            int scheduleEntryId,
            ScheduleService scheduleService,
            HttpContext context)
        {
            var id = int.Parse(context.User.FindFirstValue("UserId"));
            var result = await scheduleService.DeleteUserFromSchedule(scheduleId, scheduleEntryId, id);

            if (result)
            {
                return Results.Ok(new BasicResponse<string>(StatusCode.Success, "User removed from table"));
            }
            else
            {
                return Results.Ok(new BasicResponse<string>(StatusCode.BadRequest, "User does not exist in the schedule"));
            }
        }
    }
}
