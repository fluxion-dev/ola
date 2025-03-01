namespace Ola.Domain.TargetResolution;

public class TargetResolutionException : Exception
{
    public TargetResolutionException(string message) : base(message) { }
    public TargetResolutionException(string message, Exception innerException) : base(message, innerException) { }
}