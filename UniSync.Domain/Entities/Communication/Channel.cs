using UniSync.Domain.Common; // Assuming a similar structure for Result<T>
using System;

namespace UniSync.Domain.Entities.Communication
{
    public class Channel
    {
        public Guid ChannelId { get; private set; }
        public string ChannelName { get; private set; }
        public Guid ServerId { get; private set; }

    }
}
