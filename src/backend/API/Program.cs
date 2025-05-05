using API.Dependency;
using API.Endpoints;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence.Providers;


var builder = WebApplication.CreateBuilder(args);

builder.AddDependency();


var app = builder.Build();


app.MapAuthEndpoints();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
