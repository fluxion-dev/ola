using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Ola.Application;

namespace Ola;

internal class ServiceShell(ILogger logger, MainService ola) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Starting Ola");
        await ExecuteAsync(cancellationToken);
    }

    private async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Attempting to resolve project ...");
        await ola.ExecuteAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Stopping Ola");
        return Task.CompletedTask;
    }
}