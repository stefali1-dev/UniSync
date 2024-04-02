using Microsoft.AspNetCore.SignalR;
using UniSync.Api.Services;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities;
using UniSync.Infrastructure;

namespace UniSync.Api.Hubs;

public class ChatHub : Hub
{
    private readonly IDictionary<string, UserRoomConnection> _connection;
    private readonly IMessageRepository _messageRepository;
    private readonly ICurrentUserService _currentUserService;

    public ChatHub(IDictionary<string, UserRoomConnection> connection, IMessageRepository messageRepository, ICurrentUserService currentUserService)
    {
        _connection = connection;
        _messageRepository = messageRepository;
        _currentUserService = currentUserService;
    }

    public async Task JoinRoom(UserRoomConnection userConnection)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, userConnection.Room!);
        _connection[Context.ConnectionId] = userConnection;
        await Clients.Group(userConnection.Room!)
            .SendAsync("ReceiveMessage", "Lets Program Bot", $"{userConnection.User} has Joined the Group", DateTime.Now);
        await SendConnectedUser(userConnection.Room!);
    }

    public async Task SendMessage(string message)
    {
        if (_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection userRoomConnection))
        {
            await Clients.Group(userRoomConnection.Room!)
                .SendAsync("ReceiveMessage", userRoomConnection.User, message, DateTime.Now);

            var _message = new Message
            {
                MessageId = Guid.NewGuid(),
                Content = message,
                SenderName = userRoomConnection.User,
                ChannelName = userRoomConnection.Room,
                Timestamp = DateTime.Now
            };

            await _messageRepository.AddAsync(_message);
        }
    }

    public override Task OnDisconnectedAsync(Exception? exp)
    {
        if (!_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection roomConnection))
        {
            return base.OnDisconnectedAsync(exp);
        }

        _connection.Remove(Context.ConnectionId);
        Clients.Group(roomConnection.Room!)
            .SendAsync("ReceiveMessage", "Lets Program bot", $"{roomConnection.User} has Left the Group", DateTime.Now);
        SendConnectedUser(roomConnection.Room!);
        return base.OnDisconnectedAsync(exp);
    }

    public Task SendConnectedUser(string room)
    {
        var users = _connection.Values
            .Where(u => u.Room == room)
            .Select(s => s.User);
        return Clients.Group(room).SendAsync("ConnectedUser", users);
    }

}

public class UserRoomConnection
{
    public string? User { get; set; }
    public string? Room { get; set; }
}