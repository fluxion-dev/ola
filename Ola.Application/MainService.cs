using Ola.Application.TargetResolution;
using Ola.Domain;
using Ola.Domain.Configuration;
using Ola.Domain.Logging;

namespace Ola.Application;

public class MainService(
    ILogger logger,
    ToolOptions options,
    TargetResolutionService targetResolutionService) : IMainService
{
    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        logger.NotifyApplicationStarted();
        var resolved = await targetResolutionService.TryResolveAsync(options.Target, cancellationToken);
        logger.NotifyTargetResolved(resolved);
        logger.NotifyApplicationFinished();
    }
}