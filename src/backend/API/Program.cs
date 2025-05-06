using API.Dependency;
using API.Endpoints;
using Application.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Persistence.Providers;


var builder = WebApplication.CreateBuilder(args);

builder.AddDependency();


var app = builder.Build();


if (builder.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.MapAuthEndpoints();
app.UseAuthentication();
app.UseAuthorization();

app.Run();
