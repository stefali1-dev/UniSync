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
        //await Clients.Group(userConnection.Room!)
        //    .SendAsync("ReceiveMessage", "Lets Program Bot", $"{userConnection.UserId} has Joined the Group", DateTime.Now);
        await SendConnectedUser(userConnection.Room!);
    }

    public async Task SendMessage(string message)
    {
        if (_connection.TryGetValue(Context.ConnectionId, out UserRoomConnection userRoomConnection))
        {
            await Clients.Group(userRoomConnection.Room!)
                .SendAsync("ReceiveMessage", userRoomConnection.UserId, userRoomConnection.Room, message, DateTime.Now);

            await Console.Out.WriteLineAsync("Receiced message: " + message);

            await Console.Out.WriteLineAsync(userRoomConnection.UserId);
            await Console.Out.WriteLineAsync(userRoomConnection.Room);


            var _message = new Message
            {
                MessageId = Guid.NewGuid(),
                Content = message,
                Timestamp = DateTime.Now,
                ChatUserId = new Guid(userRoomConnection.UserId),
                ChannelId = new Guid(userRoomConnection.Room)
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
        //Clients.Group(roomConnection.Room!)
        //    .SendAsync("ReceiveMessage", "Lets Program bot", $"{roomConnection.UserId} has Left the Group", DateTime.Now);
        SendConnectedUser(roomConnection.Room!);
        return base.OnDisconnectedAsync(exp);
    }

    public Task SendConnectedUser(string room)
    {
        var users = _connection.Values
            .Where(u => u.Room == room)
            .Select(s => s.UserId);
        return Clients.Group(room).SendAsync("ConnectedUser", users);
    }

}

public class UserRoomConnection
{
    public string? UserId { get; set; }
    public string? Room { get; set; }
}