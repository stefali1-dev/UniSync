using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Features.Messages.Queries.GetByGroup;

namespace UniSync.Api.Controllers;

[ApiController]
public class MessagesController : ApiControllerBase
{

    //[Authorize(Roles = "User")]
    [HttpGet("ByChannel/{channel}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessagesByChannel(string channel)
    {
        var query = new GetByChannelMessagesQuery { Channel = channel };
        var result = await Mediator.Send(query);
        if (!result.Success)
        {
            if (result.Message == $"Messages from channel {channel} not found")
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }
        return Ok(result);
    }
}