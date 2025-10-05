using System.Buffers;
using System.Text;
using DotPulsar;
using DotPulsar.Abstractions;
using DotPulsar.Extensions;

namespace ApachePulsar.Services;

public class PulsarConsumerService : IPulsarConsumerService
{
    private readonly IPulsarClient _client;
    private readonly IConsumer<System.Buffers.ReadOnlySequence<byte>> _consumer;

    public PulsarConsumerService()
    {
        _client = PulsarClient.Builder()
            .ServiceUrl(new Uri("pulsar://localhost:6650"))
            .Build();

        _consumer = _client.NewConsumer()
            .SubscriptionName("my-subscription")
            .Topic("persistent://public/default/my-topic")
            .Create();
    }

    public async Task<string> ConsumeMessageAsync()
    {
        var message = await _consumer.Receive();
        string receivedMessage = Encoding.UTF8.GetString(message.Data);
        await _consumer.Acknowledge(message);
        
        return receivedMessage;
    }
    
    public async Task<IEnumerable<MessageId>> MessageIdsAsync()
    {
        var message = await _consumer.GetLastMessageIds();
        
        return message;
    }

    public  IAsyncEnumerable<IMessage<ReadOnlySequence<byte>>>  MessageList()
    {
        return _consumer.Messages();
        
    }
}