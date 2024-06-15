using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniSync.Application.Contracts.Interfaces;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities;

namespace UniSync.Application.Features.Channels
{
    public class ChannelService : IChannelService
    {
        private readonly IChannelRepository channelRepository;
        private readonly IChatUserRepository chatUserRepository;

        public ChannelService(IChannelRepository channelRepository, IChatUserRepository chatUserRepository)
        {
            this.channelRepository = channelRepository;
            this.chatUserRepository = chatUserRepository;
        }

        public async Task AddChannel(ChannelCreationDto channelCreationDto)
        {
            // Create a new channel with a generated ID and name from DTO
            var newChannel = new Channel
            {
                ChannelId = Guid.NewGuid(),
                ChannelName = channelCreationDto.ChannelName,
                Messages = new List<Message>() // Initialize Messages as an empty list
            };

            // Attach users to the channel using provided user IDs
            foreach (var userId in channelCreationDto.ChatUsersIds)
            {
                var response = await chatUserRepository.FindByIdAsync(new Guid(userId)); // Assuming such method exists
                var user = response.Value;
                newChannel.AttachUser(user);
            }

            // Add the new channel to the repository
            await channelRepository.AddAsync(newChannel);
        }
    }
}
