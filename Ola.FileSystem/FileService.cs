using Ola.Domain;
using Ola.Domain.FileSystem;

namespace Ola.FileSystem;

public class FileService : IFileService
{
    public bool FileExists(string path) => File.Exists(path);

    public bool PathExists(string path) => Path.Exists(path);
    public bool DirectoryExists(string path) => Directory.Exists(path);
    
    public bool TryFindByExtension(string path, string extension, out string fullPath)
    {
        if (!DirectoryExists(path)) throw new DirectoryNotFoundException($"Directory not found: {path}");
        
        var files = Directory.GetFiles(path, $"*{extension}", SearchOption.TopDirectoryOnly);

        if (files.Length == 0)
        {
            fullPath = string.Empty;
            return false;
        }
        
        fullPath = files[0];
        return true;
    }
}