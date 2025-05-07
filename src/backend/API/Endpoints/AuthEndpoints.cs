using API.Contracts;
using Application.Enums;
using Application.Interfaces;
using Application.Models;
using Domain.Entities;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using System.Threading.Tasks;

namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.Map("/login", Login);
            app.Map("/register", Register);
            app.Map("/register-business", RegisterBusiness);
        }

        public static async Task<IResult> Login(
            RequestLogin request,
            IUserService userService,
            HttpContext context)
        {
            BasicResponse<string> response = await userService.Login(request.Login, request.Password);
            context.Response.Cookies.Append("crumble-cookies", response.Data);
            return Results.Ok();
        }

        public async static Task<IResult> Register(
            RequestRegister request,
            IValidator<RequestRegister> validator,
            IUserService userService)
        {
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return Results.BadRequest(new BasicResponse<List<FluentValidation.Results.ValidationFailure>>(StatusCode.BadRequest, "Validation problem", result.Errors));
            }
            await userService.Register(request.ToUserModel());
            return Results.Ok();
        }

        public async static Task<IResult> RegisterBusiness(
            RequestRegister request,
            IValidator<RequestRegister> validator,
            IUserService userService)
        {
            var result = await validator.ValidateAsync(request);
            if (!result.IsValid)
            {
                return Results.BadRequest(new BasicResponse<List<FluentValidation.Results.ValidationFailure>>(StatusCode.BadRequest, "Validation problem", result.Errors));
            }
            await userService.RegisterBusiness(request.ToUserModel(), request.ToBusinessInfo());
            return Results.Ok();
        }
    }
}
