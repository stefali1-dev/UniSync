using UniSync.Domain.Common;

namespace UniSync.Domain.Entities.Communication
{
    public class Server
    {
        public Guid ServerId { get; private set; }
        public string ServerName { get; private set; }
        public string Description { get; private set; }

        public List<Channel> Channels { get; private set; }

        private Server(Guid serverId, string serverName, string description)
        {
            ServerId = serverId;
            ServerName = serverName;
            Description = description;
            Channels = new List<Channel>();
        }

        public static Result<Server> Create(Guid serverId, string serverName, string description)
        {
            if (serverId == Guid.Empty)
            {
                return Result<Server>.Failure("ServerId cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(serverName))
            {
                return Result<Server>.Failure("ServerName cannot be empty or whitespace.");
            }

            if (string.IsNullOrWhiteSpace(description))
            {
                return Result<Server>.Failure("Description cannot be empty or whitespace.");
            }

            var server = new Server(serverId, serverName, description);
            return Result<Server>.Success(server);
        }
    }
}
