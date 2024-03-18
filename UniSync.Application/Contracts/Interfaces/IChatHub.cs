using Microsoft.AspNetCore.SignalR;
using UniSync.Application.Features.Users.Queries;

namespace UniSync.Application.Contracts.Interfaces
{
    public interface IChatHub
    {
        public Task SendPrivate(string receiverName, string message);
        public Task Join(string roomName);
        public Task Leave(string roomName);
        public IEnumerable<UserDto> GetUsers(string roomName);


    }
}
