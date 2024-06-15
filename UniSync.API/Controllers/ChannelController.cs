using Microsoft.AspNetCore.Mvc;
using UniSync.API.Controllers;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Features.Channels;
using UniSync.Application.Features.Channels.Commands.CreateChannel;
using UniSync.Application.Features.Channels.Queries;
using UniSync.Domain.Entities;

namespace UniSync.Api.Controllers;

[ApiController]
public class ChannelController : ApiControllerBase
{
    private readonly IChannelService channelService;

    public ChannelController(IChannelService channelService)
    {
        this.channelService = channelService;
    }

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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddChannel([FromBody] ChannelCreationDto channelCreationDto)
    {
        try
        {

            await channelService.AddChannel(channelCreationDto);
            return Ok(new { message = $"Channel '{channelCreationDto.ChannelName}' created successfully." });
        }
        catch (Exception ex)
        {
            // Log the exception details here as needed
            return BadRequest(new { error = $"An error occurred while creating the channel: {ex.Message}" });
        }
    }

}