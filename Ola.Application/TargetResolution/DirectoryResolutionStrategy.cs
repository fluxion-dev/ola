using Ola.Domain.FileSystem;
using Ola.Domain.TargetResolution;

namespace Ola.Application.TargetResolution;

internal class DirectoryResolutionStrategy(IFileService fileService) : ResolutionStrategy
{
    public override Task<string> ResolveAsync(string target, CancellationToken cancellationToken)
    {
        try
        {
            if (fileService.TryFindByExtension(target, ".sln", out var slnFile))
                return Task.FromResult(slnFile);
        
            if (fileService.TryFindByExtension(target, ".csproj", out var csprojFile))
                return Task.FromResult(csprojFile);
        }
        catch (DirectoryNotFoundException de)
        {
            throw new TargetResolutionException("Unable to resolve directory.", de);
        }
        
        throw new TargetResolutionException("Unable to resolve solution or project file in target directory",
            new FileNotFoundException(target));
    }
}