namespace Ola.Domain;

public interface IMainService
{
    Task ExecuteAsync(CancellationToken cancellationToken);
}