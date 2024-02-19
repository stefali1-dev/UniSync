using API.DTOs;
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

    // TODO: POST, PUT, DELETE methods
}
