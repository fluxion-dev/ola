namespace Ola.Domain.Logging;

public interface ILogger
{
    public void NotifyApplicationStarted();
    public void NotifyTargetResolved(string target);
    void NotifyApplicationFinished();
}