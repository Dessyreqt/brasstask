namespace BrassTask.Api.Tests;

using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IRequest = Microsoft.VisualStudio.TestPlatform.ObjectModel.Client.IRequest;

[CollectionDefinition(nameof(TestFixture))]
public class SliceFixtureCollection : ICollectionFixture<TestFixture>
{
}

public class TestFixture : IAsyncLifetime
{
    private readonly IHost _host;
    private readonly IServiceScopeFactory _scopeFactory;
    private Action? _customDependencyConfigurator;

    public TestFixture()
    {
        _host = Host.CreateDefaultBuilder().ConfigureServices(TestServices.Configure).Build();
        _scopeFactory = _host.Services.GetRequiredService<IServiceScopeFactory>();
    }

    public void UseCustomDependencies(Action customDependencyConfigurator)
    {
        _customDependencyConfigurator = customDependencyConfigurator;
    }

    public Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
    {
        return ExecuteScopeAsync(
            services =>
            {
                var mediator = services.GetRequiredService<IMediator>();

                return mediator.Send(request);
            });
    }

    public Task SendAsync(IRequest request)
    {
        return ExecuteScopeAsync(
            services =>
            {
                var mediator = services.GetRequiredService<IMediator>();

                return mediator.Send(request);
            });
    }

    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        _host.Dispose();
        return Task.CompletedTask;
    }

    private async Task<T> ExecuteScopeAsync<T>(Func<IServiceProvider, Task<T>> action)
    {
        TestDependencies.Initialize();

        _customDependencyConfigurator?.Invoke();

        using var scope = _scopeFactory.CreateScope();
        var result = await action(scope.ServiceProvider);

        _customDependencyConfigurator = null;

        return result;
    }

    private async Task ExecuteScopeAsync(Func<IServiceProvider, Task> action)
    {
        TestDependencies.Initialize();

        _customDependencyConfigurator?.Invoke();

        using var scope = _scopeFactory.CreateScope();
        await action(scope.ServiceProvider);

        _customDependencyConfigurator = null;
    }
}
