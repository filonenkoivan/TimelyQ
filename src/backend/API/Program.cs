using API.BackgroundServices;
using API.Dependency;
using API.Endpoints;
using API.RealTimeDashboard;
using Application.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence.Providers;


var builder = WebApplication.CreateBuilder(args);
var EmailApiKey = builder.Configuration["Email:password"];


builder.AddDependency();


var app = builder.Build();



if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapAuthEndpoints();
app.MapScheduleEndpoints();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.UseHangfireDashboard("/hangfiredash");
app.MapHub<DashboardHub>("/dashboard");

RecurringJob.AddOrUpdate<BackgroundEmailSender>("background sender", x => x.EmailSender(), Cron.MinuteInterval(15));

app.Run();

