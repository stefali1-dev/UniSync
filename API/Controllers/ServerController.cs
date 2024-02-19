using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers;

public class ServerController : BaseApiController
{
    private readonly IServerRepository _serverRepository;
    private readonly IMapper _mapper;

    public ServerController(IServerRepository serverRepository, IMapper mapper)
    {
        _serverRepository = serverRepository;
        _mapper = mapper;
    }

    // Get all servers 
    [HttpGet] // GET: api/server
    public async Task<ActionResult<IEnumerable<ServerDto>>> GetServers()
    {
        var servers = await _serverRepository.GetServersAsync();
        var serversToReturn = _mapper.Map<IEnumerable<ServerDto>>(servers);
        return Ok(serversToReturn);
    }

    // Get a single server by id
    [HttpGet("{id}")]
    public async Task<ActionResult<ServerDto>> GetServer(int id)
    {
        var server = await _serverRepository.GetServerByIdAsync(id);
        if (server == null) return NotFound();
        return _mapper.Map<ServerDto>(server);
    }

    // Create a new server
    [HttpPost]
    public async Task<ActionResult<ServerDto>> CreateServer(ServerDto serverDto)
    {
        var server = _mapper.Map<Server>(serverDto);
        _serverRepository.Add(server);
        if (await _serverRepository.SaveAllAsync()) 
            return Ok(_mapper.Map<ServerDto>(server));
        return BadRequest("Failed to create the server");
    }

    // Update an existing server
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateServer(int id, ServerDto serverDto)
    {
        var server = await _serverRepository.GetServerByIdAsync(id);
        if (server == null) return NotFound();

        // Map the updated properties
        _mapper.Map(serverDto, server);

        _serverRepository.Update(server);
        if (await _serverRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to update the server");
    }

    // Delete a server
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteServer(int id)
    {
        var server = await _serverRepository.GetServerByIdAsync(id);
        if (server == null) return NotFound();

        _serverRepository.Delete(server);
        if (await _serverRepository.SaveAllAsync()) return NoContent();
        return BadRequest("Failed to delete the server");
    }
}
