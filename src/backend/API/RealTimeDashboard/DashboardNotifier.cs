using Application.Interfaces;
using Application.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Persistence.DataBaseContext;

namespace API.RealTimeDashboard
{
    public class DashboardNotifier(IHubContext<DashboardHub, SendDashBoardAsync> context, AppDbContext db) : IDashboardNotifier
    {
        public async Task NotifyScheduleUpdate(int scheduleId)
        {
            var schedule = await db.Schedule.Include(x=>x.ScheduleEntries).FirstOrDefaultAsync(x => x.Id == scheduleId);
            await context.Clients.All.SendDashBoard(schedule);
        }
    }
}
