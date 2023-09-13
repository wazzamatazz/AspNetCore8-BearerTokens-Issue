using System.Security.Claims;

using Microsoft.AspNetCore.Authentication.BearerToken;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication()
    .AddBearerToken();

builder.Services.AddAuthorization();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapPost("/login", () => Results.SignIn(new ClaimsPrincipal(new ClaimsIdentity(new[] { 
    new Claim("iss", "my-app"), 
    new Claim("sub", "test-user@my-app"),
}, BearerTokenDefaults.AuthenticationScheme))));

app.MapGet("/me", (HttpContext context) => context.User.Claims.Select(x => new { x.Type, x.Value }).ToArray())
    .RequireAuthorization();

app.Run();
