using UniSync.Domain.Common;

namespace UniSync.Domain.Entities.Communication
{
    public class Server
    {
        public Guid ServerId { get; private set; }
        public string ServerName { get; private set; }
        public string Description { get; private set; }

        public List<Channel> Channels { get; private set; }

    }
}
