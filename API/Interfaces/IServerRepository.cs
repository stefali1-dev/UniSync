using API.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Interfaces;
public interface IServerRepository
{
    Task<Server> GetServerByIdAsync(int id);
    Task<IEnumerable<Server>> GetServersAsync();
    void Add(Server server);
    void Update(Server server);
    void Delete(Server server);
    Task<bool> SaveAllAsync();
    Task<Channel> GetChannelByIdAsync(int serverId, int channelId);
    Task<IEnumerable<Channel>> GetChannelsByServerIdAsync(int serverId);
    void AddChannel(Channel channel);
    void DeleteChannel(Channel channel);
    void UpdateChannel(Channel channel);
}