using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace API.RealTimeDashboard
{
    public class DashboardHub(ScheduleService services) : Hub<SendDashBoardAsync>
    {
        //public async Task GetDashboard()
        //{
        //    try
        //    {
        //        var schedule = await services.GetSchedule(1);
        //        await Clients.All.SendDashBoard(schedule);
        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex);
        //    }

        //}
    }


    public interface SendDashBoardAsync
    {
        public Task SendDashBoard(Schedule schedule);
    }
}
