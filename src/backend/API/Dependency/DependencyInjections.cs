using API.Contracts.User;
using API.Validation;
using Application.Interfaces;
using Application.Interfaces.Repository;
using Application.Services;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistence.Configurations;
using Persistence.DataBaseContext;
using Persistence.Providers;
using Persistence.Repository;
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
            builder.Services.AddScoped(typeof(IUserService<>), typeof(UserService<>));
            builder.Services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));
            builder.Services.AddScoped<IJwtProvider, JwtProvider>();
            builder.Services.AddScoped<IUserRepository<User>, UserRepository<User>>();
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.SecurityKey))
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
