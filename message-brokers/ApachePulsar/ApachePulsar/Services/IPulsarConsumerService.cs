using System.Buffers;
using DotPulsar;
using DotPulsar.Abstractions;

namespace ApachePulsar.Services;

public interface IPulsarConsumerService
{
    Task<string> ConsumeMessageAsync();
    Task<IEnumerable<MessageId>> MessageIdsAsync();
    IAsyncEnumerable<IMessage<ReadOnlySequence<byte>>> MessageList();
}