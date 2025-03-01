namespace Ola.Domain.TargetResolution;

public interface ITargetResolutionService
{
    Task<string> TryResolveAsync(string target, CancellationToken cancellationToken);
}