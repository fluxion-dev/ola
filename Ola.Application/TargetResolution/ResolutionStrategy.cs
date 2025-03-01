namespace Ola.Application.TargetResolution;

internal abstract class ResolutionStrategy
{
    public abstract Task<string> ResolveAsync(string target, CancellationToken cancellationToken);
}