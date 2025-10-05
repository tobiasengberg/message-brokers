using ApachePulsar.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApachePulsar.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PulsarController : ControllerBase
{
    private readonly IPulsarService _pulsarService;
    private readonly IPulsarConsumerService _pulsarConsumerService;
    
    public PulsarController(IPulsarService pulsarService, IPulsarConsumerService pulsarConsumerService)
    {
        _pulsarService = pulsarService;
        _pulsarConsumerService = pulsarConsumerService;
    }

    [HttpPost]
    public async Task<IActionResult> SendMessage([FromBody] string message)
    {
        await _pulsarService.SendMessageAsync(message);
        return Ok("Message sent to Pulsar");
    }
    
    [HttpGet("consume")]
    public async Task<IActionResult> ConsumeMessage()
    {
        string message = await _pulsarConsumerService.ConsumeMessageAsync();
        return Ok(message);
    }
    
    [HttpGet("ids")]
    public async Task<IActionResult> GetMessageIds()
    {
        var message = await _pulsarConsumerService.MessageIdsAsync();
        return Ok(message);
    }
    
    [HttpGet("messages")]
    public async Task<IActionResult> GetMessages()
    {
        var message = _pulsarConsumerService.MessageList();
        return Ok(message);
    }
}