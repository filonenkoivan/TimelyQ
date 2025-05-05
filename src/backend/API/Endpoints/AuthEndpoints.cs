using Application.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using System.Threading.Tasks;

namespace API.Endpoints
{
    public static class AuthEndpoints
    {
        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            app.Map("/", Login);
        }

        public static async Task<IResult> Login(IUserService userService, HttpContext context)
        {
            var token = userService.Login("asa","sas");

            context.Response.Cookies.Append("crumble-cookies", token);

            return Results.Ok();
        }

        public static IResult Register(RegisterRequest request)
        {
            return Results.Ok();
        }
    }
}
