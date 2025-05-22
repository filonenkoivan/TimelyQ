using API.Contracts;
using API.RealTimeDashboard;
using API.Validation;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Services;
using Domain.Entities;
using FluentValidation;
using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Configurations;
using Persistence.DataBaseContext;
using Persistence.Providers;
using Persistence.Repository;
using System.Security.Claims;
using System.Text;

namespace API.Dependency
{
    public static class DependencyInjections
    {
        public static void AddDependency(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JwtConfiguration"));

            var config = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>();
            builder.Services.AddScoped<IValidator<RequestRegister>, UserCustomValidation>();
            builder.Services.AddTransient<IEmailSenderService, EmailSenderService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IScheduleRepository, ScheduleRepository>();
            builder.Services.AddScoped<ScheduleService>();
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IDashboardNotifier, DashboardNotifier>();
            builder.Services.AddHangfire(x => x.UsePostgreSqlStorage(builder.Configuration.GetConnectionString("Timelyq")));
            builder.Services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = builder.Configuration.GetConnectionString("Redis");
                opt.InstanceName = "SampleApp";
                // звернути увагу на цей нейм, вказано щось про ключі
            });
            builder.Services.AddHangfireServer();
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("http://localhost:5173");
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                    policy.AllowCredentials();

                });
            });
            builder.Services.AddSignalR();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                string connectionString = builder.Configuration.GetConnectionString("Timelyq");
                options.UseNpgsql(connectionString);
            });

            builder.Services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecurityKey)),
                    RoleClaimType = ClaimTypes.Role
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["crumble-cookies"];
                        return Task.CompletedTask;
                    }
                };
            });
            builder.Services.AddAuthorization();

        }
    }
}
