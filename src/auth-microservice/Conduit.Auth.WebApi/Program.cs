using Conduit.Auth.ApplicationLayer;
using Conduit.Auth.ApplicationLayer.Users.GetCurrent;
using Conduit.Auth.ApplicationLayer.Users.Register;
using Conduit.Auth.Domain.Services;
using Conduit.Auth.Domain.Services.ApplicationLayer.Users;
using Conduit.Auth.Domain.Users.Passwords;
using Conduit.Auth.Domain.Users.Services;
using Conduit.Auth.Infrastructure.Dapper.DependencyInjection;
using Conduit.Auth.Infrastructure.JwtTokens;
using Conduit.Auth.Infrastructure.Services;
using Conduit.Auth.Infrastructure.Users.Passwords;
using Conduit.Auth.Infrastructure.Users.Services;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

#region ServicesConfiguration

var services = builder.Services;
var environment = builder.Environment;
var configuration = builder.Configuration;

services.AddControllers();
services.AddSwaggerGen(
    c =>
    {
        c.SwaggerDoc(
            "v1",
            new() { Title = "Conduit.Auth.WebApi", Version = "v1" });
    });

services.AddDapper(configuration.GetSection("Dapper").Bind)
    .AddJwtServices(configuration.GetSection("Jwt").Bind)
    .AddW3CLogging(configuration.GetSection("W3C").Bind)
    .AddHttpClient()
    .AddTransient(typeof(IPipelineBehavior<,>), typeof(PipelineLogger<,>))
    .AddTransient<IPasswordManager, PasswordManager>()
    .AddSingleton<IIdManager, IdManager>()
    .AddSingleton<IImageChecker, ImageChecker>()
    .AddHttpContextAccessor()
    .AddScoped<ICurrentUserProvider, CurrentUserProvider>()
    .AddMediatR(typeof(GetCurrentUserRequestHandler).Assembly)
    .AddValidatorsFromAssembly(typeof(RegisterUserModelValidator).Assembly);

#endregion

var app = builder.Build();

#region AppConfiguration

if (environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(
        c => c.SwaggerEndpoint(
            "/swagger/v1/swagger.json",
            "Conduit.Auth.WebApi v1"));
    IdentityModelEventSource.ShowPII = true;
}

app.UseW3CLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var initializationScope = app.Services.CreateScope();
await initializationScope.InitializeDapperAsync();

#endregion

app.Run();