using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/servers/{serverId}/channels")]
public class ChannelController : BaseApiController
{
    private readonly IServerRepository _serverRepository;
    private readonly IMapper _mapper;

    public ChannelController(IServerRepository serverRepository, IMapper mapper)
    {
        _serverRepository = serverRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ChannelDto>>> GetChannels(int serverId)
    {
        var channels = await _serverRepository.GetChannelsByServerIdAsync(serverId);
        return Ok(_mapper.Map<IEnumerable<ChannelDto>>(channels));
    }

    [HttpGet("{channelId}")]
    public async Task<ActionResult<ChannelDto>> GetChannel(int serverId, int channelId)
    {
        var channel = await _serverRepository.GetChannelByIdAsync(serverId, channelId);
        if (channel == null) return NotFound();
        return _mapper.Map<ChannelDto>(channel);
    }
    
    [HttpPost]
    public async Task<ActionResult<ChannelDto>> CreateChannel(int serverId, ChannelDto channelDto)
    {
        var server = await _serverRepository.GetServerByIdAsync(serverId);
        if (server == null) return NotFound($"Server with ID {serverId} not found.");

        var channel = _mapper.Map<Channel>(channelDto);
        channel.ServerId = serverId; // Ensure the channel is associated with the correct server

        _serverRepository.AddChannel(channel);
        if (await _serverRepository.SaveAllAsync())
        {
            var returnDto = _mapper.Map<ChannelDto>(channel);
            return CreatedAtAction(nameof(GetChannel),
                new { serverId = serverId, channelId = channel.ChannelId }, returnDto);
        }

        return BadRequest("Failed to create the channel");
    }

    [HttpPut("{channelId}")]
    public async Task<IActionResult> UpdateChannel(int serverId, int channelId, ChannelDto channelDto)
    {
        var channel = await _serverRepository.GetChannelByIdAsync(serverId, channelId);
        if (channel == null) return NotFound($"Channel with ID {channelId} in server ID {serverId} not found.");

        _mapper.Map(channelDto, channel); // Apply the updated properties to the existing channel entity

        _serverRepository.UpdateChannel(channel);
        if (await _serverRepository.SaveAllAsync())
        {
            return NoContent(); // Indicates successful update without returning data
        }

        return BadRequest("Failed to update the channel");
    }

    [HttpDelete("{channelId}")]
    public async Task<IActionResult> DeleteChannel(int serverId, int channelId)
    {
        var channel = await _serverRepository.GetChannelByIdAsync(serverId, channelId);
        if (channel == null) return NotFound($"Channel with ID {channelId} in server ID {serverId} not found.");

        _serverRepository.DeleteChannel(channel);
        if (await _serverRepository.SaveAllAsync())
        {
            return NoContent(); // Indicates successful deletion without returning data
        }

        return BadRequest("Failed to delete the channel");
    }

}
