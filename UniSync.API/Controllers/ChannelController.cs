using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Features.Channels.Queries;
using UniSync.Domain.Entities;

namespace UniSync.Api.Controllers;

[ApiController]
public class ChannelController : ApiControllerBase
{
    [HttpGet("ByUserId/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChannelsByUserId(string id)
    {
        var query = new GetChannelsByUserIdQuery { UserId = new Guid(id) };
        var result = await Mediator.Send(query);
        if (!result.Success)
        {
            if (result.Message == $"Channels from user {id} not found")
            {
                return NotFound(result);
            }
            return BadRequest(result);
        }
        return Ok(result);
    }
}