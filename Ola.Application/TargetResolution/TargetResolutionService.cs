using Ola.Domain.FileSystem;
using Ola.Domain.TargetResolution;

namespace Ola.Application.TargetResolution;

public class TargetResolutionService(IFileService fileService) : ITargetResolutionService
{
    public async Task<string> TryResolveAsync(string target, CancellationToken cancellationToken)
    {
        ResolutionStrategy resolutionStrategy = new FileResolutionStrategy(fileService);
        
        if (Path.EndsInDirectorySeparator(target) || DirectoryExists(target))
            resolutionStrategy = new DirectoryResolutionStrategy(fileService);

        return await resolutionStrategy.ResolveAsync(target, cancellationToken);
    }
    
    private bool DirectoryExists(string target) => fileService.DirectoryExists(target);
}