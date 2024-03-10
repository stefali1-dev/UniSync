using UniSync.Domain.Common; // Assuming a similar structure for Result<T>
using System;

namespace UniSync.Domain.Entities.Communication
{
    public class Channel
    {
        public Guid ChannelId { get; private set; }
        public string ChannelName { get; private set; }
        public Guid ServerId { get; private set; }

        // Constructor
        private Channel(Guid channelId, string channelName, Guid serverId)
        {
            ChannelId = channelId;
            ChannelName = channelName;
            ServerId = serverId;
        }

        // Create method
        public static Result<Channel> Create(Guid channelId, string channelName, Guid serverId)
        {
            if (channelId == Guid.Empty)
            {
                return Result<Channel>.Failure("ChannelId cannot be empty.");
            }

            if (string.IsNullOrWhiteSpace(channelName))
            {
                return Result<Channel>.Failure("ChannelName cannot be empty or whitespace.");
            }

            if (serverId == Guid.Empty)
            {
                return Result<Channel>.Failure("ServerId cannot be empty.");
            }

            var channel = new Channel(channelId, channelName, serverId);
            return Result<Channel>.Success(channel);
        }
    }
}
