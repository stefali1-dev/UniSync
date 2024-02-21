using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class ServerRepository : IServerRepository
{
    private readonly DataContext _context;

    public ServerRepository(DataContext context)
    {
        _context = context;
    }

    public async Task<Server> GetServerByIdAsync(int id)
    {
        return await _context.Servers.FindAsync(id);
    }

    public async Task<IEnumerable<Server>> GetServersAsync()
    {
        return await _context.Servers.ToListAsync();
    }

    public void Update(Server server)
    {
        _context.Entry(server).State = EntityState.Modified;
    }

    public async Task<bool> SaveAllAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
    
    public void Add(Server server)
    {
        _context.Servers.Add(server);
    }

    public void AddChannel(Channel channel)
    {
        _context.Channels.Add(channel);
    }

    public void Delete(Server server)
    {
        _context.Servers.Remove(server);
    }

    public void DeleteChannel(Channel channel)
    {
        _context.Channels.Remove(channel);
    }

    public async Task<Channel> GetChannelByIdAsync(int serverId, int channelId)
    {
        return await _context.Channels.FindAsync(serverId, channelId);
    }

    public async Task<IEnumerable<Channel>> GetChannelsByServerIdAsync(int serverId)
    {
        return await _context.Channels.Where(x => x.ServerId == serverId).ToListAsync();
    }
    
    public void UpdateChannel(Channel channel)
    {
        _context.Entry(channel).State = EntityState.Modified;
    }
}