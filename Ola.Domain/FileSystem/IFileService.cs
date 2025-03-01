namespace Ola.Domain.FileSystem;

public interface IFileService
{
    bool FileExists(string path);
    bool PathExists(string path);
    bool DirectoryExists(string path);
    bool TryFindByExtension(string path, string extension, out string fullPath);
}