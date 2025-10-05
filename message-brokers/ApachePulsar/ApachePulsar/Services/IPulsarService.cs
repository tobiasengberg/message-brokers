namespace ApachePulsar.Services;

public interface IPulsarService
{
    Task SendMessageAsync(string message);
}