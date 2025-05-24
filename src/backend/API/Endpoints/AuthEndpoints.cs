using API.Contracts;
using Application.Enums;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Caching.Distributed;
using System.Threading.Tasks;

namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.MapPost("/login", Login);
            app.MapPost("/register", Register);
            app.MapPost("/register-business", RegisterBusiness);
            app.MapGet("/restore-password", RestorePassword);
            app.MapGet("/restore-password-confirm", ConfirmRestorePassword);
        }

        public static async Task<IResult> Login(
            RequestLogin request,
            IUserService userService,
            HttpContext context)
        {
            BasicResponse<string> response = await userService.Login(request.Login, request.Password);
            context.Response.Cookies.Append("crumble-cookies", response.Data);

            return Results.Ok(response);
        }

        public async static Task<IResult> Register(
            RequestRegister request,
            IValidator<RequestRegister> validator,
            IEmailSenderService emailSenderService,
            IUserService userService
            )
        {
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return Results.BadRequest(new BasicResponse<List<FluentValidation.Results.ValidationFailure>>(StatusCode.BadRequest, "Validation problem", result.Errors));
            }

            var response = await userService.Register(request.ToUserModel());
            if(response.StatusCode == StatusCode.Success)
            {
                await emailSenderService.SendEmailAsync(request.Email, "Register", "Welcome to Timelyq!");
            }

            return Results.Ok(response);
        }

        public async static Task<IResult> RegisterBusiness(
            RequestRegister request,
            IValidator<RequestRegister> validator,
            IUserService userService,
            IEmailSenderService emailSenderService)
        {
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return Results.BadRequest(new BasicResponse<List<FluentValidation.Results.ValidationFailure>>(StatusCode.BadRequest, "Validation problem", result.Errors));
            }

            var response = await userService.RegisterBusiness(request.ToUserModel(), request.ToBusinessInfo());
            if (response.StatusCode == StatusCode.Success)
            {
                await emailSenderService.SendEmailAsync(request.Email, "Register", "Welcome to Timelyq!");
            }
            return Results.Ok(response);
        }

        public async static Task<IResult> RestorePassword(
            string email,
            IUserService service,
            IEmailSenderService emailSenderService,
            IDistributedCache cache
            )
        {
            TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);

            var user = await service.GetUserAsync(email);
            Random random = new Random();
            string randomNumber = random.Next(1000, 4000).ToString();
            
            await emailSenderService.SendEmailAsync(user.Email, "password reset", randomNumber);
            await cache.SetStringAsync("reset-code", randomNumber);
            await cache.SetStringAsync("reset-code-email", user.Email);

            return Results.Ok(user);
        }

        public async static Task<IResult> ConfirmRestorePassword(
        string code,
        string email,
        IUserService service,
        IEmailSenderService emailSenderService,
        IDistributedCache cache
    )
        {
            TimeSpan _cacheExpiry = TimeSpan.FromMinutes(5);

            var user = await service.GetUserAsync(email);
            string verifyCode = await cache.GetStringAsync("reset-code");


            if (code == verifyCode)
            {
                return Results.Ok("Good request, next page");
                //new password logic
            }
            else if (verifyCode == null)
            {
                return Results.BadRequest("old code");
            }

            return Results.BadRequest("invalid code");
        }
    }
}
