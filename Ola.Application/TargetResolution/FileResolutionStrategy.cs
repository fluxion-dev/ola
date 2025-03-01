using Ola.Domain.FileSystem;
using Ola.Domain.TargetResolution;

namespace Ola.Application.TargetResolution;

internal class FileResolutionStrategy(IFileService fileService) : ResolutionStrategy
{
    public override Task<string> ResolveAsync(string target, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested) 
            return Task.FromCanceled<string>(cancellationToken);

        if (!FileExists(target))
            throw new TargetResolutionException("Specified file does not exist", 
                new FileNotFoundException(target));

        if (!TargetIsProjectOrSolution(target))
            throw new TargetResolutionException("Specified file is not a project or solution", 
                new ArgumentOutOfRangeException(target));

        return Task.FromResult(target);
    }

    private bool FileExists(string target) => fileService.FileExists(target);

    private static bool TargetIsProjectOrSolution(string target)
    {
        string[] allowedExtensions = [".csproj", ".sln"];
        var extension = Path.GetExtension(target);
        return allowedExtensions.Contains(extension);
    }
}
