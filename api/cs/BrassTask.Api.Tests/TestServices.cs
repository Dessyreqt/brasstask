namespace BrassTask.Api.Tests;

using BrassTask.Api.Domain;
using BrassTask.Api.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class TestServices
{
    public static void Configure(HostBuilderContext context, IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(ScheduledTask).Assembly));

        services.AddScoped<UserFacade>();

        services.AddScoped(_ => TestDependencies.CryptoService);
        services.AddScoped(_ => TestDependencies.TaskRepository);
        services.AddScoped(_ => TestDependencies.TokenService);
        services.AddScoped(_ => TestDependencies.UserRepository);
    }
}
