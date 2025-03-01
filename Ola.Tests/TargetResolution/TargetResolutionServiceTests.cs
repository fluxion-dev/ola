using FluentAssertions;
using NSubstitute;
using Ola.Application.TargetResolution;
using Ola.Domain.FileSystem;
using Ola.Domain.TargetResolution;

namespace Ola.Tests.TargetResolution;

public class TargetResolutionServiceTests
{
    private readonly IFileService _mockFileService = Substitute.For<IFileService>();
    private readonly TargetResolutionService _subject;

    public static IEnumerable<object[]> SupportedExtensions = [["csproj"], ["sln"]];

    public TargetResolutionServiceTests()
    {
        _subject = new TargetResolutionService(_mockFileService);
    }
    
    [Theory]
    [MemberData(nameof(SupportedExtensions))]
    public async Task when_resolving_non_existent_valid_target_throws_target_resolution_exception(string ext)
    {
        var target = $"{Some.String}.{ext}";
        _mockFileService.FileExists(target).Returns(false);
        
        var badCall = () => 
            _subject.TryResolveAsync(target, CancellationToken.None);

        await badCall.Should()
            .ThrowAsync<TargetResolutionException>()
            .WithMessage("Specified file does not exist")
            .WithInnerException(typeof(FileNotFoundException), target);
    }

    [Fact]
    public async Task when_resolving_target_with_unknown_extension_throws_target_resolution_exception()
    {
        var target = $"{Some.String}.{Guid.NewGuid()}";
        _mockFileService.FileExists(target).Returns(true);
        
        var badCall = () => 
            _subject.TryResolveAsync(target, CancellationToken.None);
        
        await badCall.Should()
            .ThrowAsync<TargetResolutionException>()
            .WithMessage("Specified file is not a project or solution")
            .WithInnerException(typeof(ArgumentOutOfRangeException), target);
    }

    [Fact]
    public async Task when_resolving_target_from_a_non_existent_directory_throws_target_resolution_exception()
    {
        var target = Some.String;
        _mockFileService.DirectoryExists(target).Returns(false);
        
        var badCall = () => 
            _subject.TryResolveAsync(target, CancellationToken.None);
        
        await badCall.Should()
            .ThrowAsync<TargetResolutionException>()
            .WithMessage("Specified file does not exist")
            .WithInnerException(typeof(FileNotFoundException), target);
    }

    [Fact]
    public async Task when_resolving_target_from_directory_that_does_not_contain_project_or_solution_throws_target_resolution_exception()
    {
        var target = Some.String;
        _mockFileService.DirectoryExists(target).Returns(true);
        _mockFileService.TryFindByExtension(target, ".sln", out _).Returns(false);
        _mockFileService.TryFindByExtension(target, ".csproj", out _).Returns(false);
        
        var badCall = () => _subject.TryResolveAsync(target, CancellationToken.None);

        await badCall.Should()
            .ThrowAsync<TargetResolutionException>()
            .WithMessage("Unable to resolve solution or project file in target directory")
            .WithInnerException(typeof(FileNotFoundException), target);
    }
}