using System.Text;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;

namespace ApachePulsar.Services;

public class PulsarService : IPulsarService
{
    private readonly IPulsarClient _client;
    private readonly IProducer<System. Buffers. ReadOnlySequence<byte>> _producer;

    public PulsarService()
    {
        _client = PulsarClient.Builder()
            .ServiceUrl(new Uri("pulsar://localhost:6650")) 
            .Build();

        _producer = _client.NewProducer()
            .Topic("persistent://public/default/my-topic")
            .Create();
    }

    public async Task SendMessageAsync(string message)
    {
        await _producer.Send(Encoding.UTF8.GetBytes(message));
    }
}