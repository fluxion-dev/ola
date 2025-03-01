using Microsoft.Extensions.Logging;
using static Ola.Domain.Logging.MessageTemplates;
using ILogger = Ola.Domain.Logging.ILogger;

namespace Ola.Logging;

public class Logger(Microsoft.Extensions.Logging.ILogger logger) : ILogger
{
    public void NotifyApplicationStarted() => logger.LogInformation(ApplicationStartedTemplate);
    public void NotifyTargetResolved(string target) => logger.LogInformation(TargetResolvedTemplate, target);
    public void NotifyApplicationFinished() => logger.LogInformation(ApplicationFinishedTemplate);
}