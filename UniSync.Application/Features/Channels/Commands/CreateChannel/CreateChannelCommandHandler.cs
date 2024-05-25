using UniSync.Application.Contracts;
using UniSync.Application.Features.Channels.Queries;
using UniSync.Application.Models;
using UniSync.Application.Persistence;
using UniSync.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UniSync.Application.Features.Channels.Commands.CreateChannel
{
    public class CreateChannelCommandHandler : IRequestHandler<CreateChannelCommand, CreateChannelCommandResponse>
    {
        private readonly IChannelRepository channelRepository;
        private readonly IChatUserRepository chatUserRepository;

        public CreateChannelCommandHandler(IChannelRepository channelRepository, IChatUserRepository chatUserRepository)
        {
            this.channelRepository = channelRepository;
            this.chatUserRepository = chatUserRepository;
        }
        public async Task<CreateChannelCommandResponse> Handle(CreateChannelCommand request, CancellationToken cancellationToken)
        {
            //var validator = new CreateChannelCommandValidator(channelRepository);
            //var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            //if (!validatorResult.IsValid)
            //{
            //    return new CreateChannelCommandResponse
            //    {
            //        Success = false,
            //        ValidationsErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
            //    };
            //}
            Channel channel;
            try {
                var userIds = request.ChatUserIds;

                var users = new List<ChatUser>();

                foreach (var userId in userIds)
                {
                    var user = await chatUserRepository.FindByIdAsync(new Guid(userId));
                    if (user != null)
                    {
                        users.Add(user.Value);
                    }
                }


                channel  = new(
                    Guid.NewGuid(),
                    request.ChannelName
                    );
                channel.AttachUsers(users);
            }
            catch ( Exception ex )
            {
                return new CreateChannelCommandResponse
                {
                    Success = false,
                    ValidationsErrors = new List<string> { ex.Message }
                };
            }

            var result = channelRepository.AddAsync(channel);


            return new CreateChannelCommandResponse
            {
                Success = true,
                Channel = new ChannelDto
                {
                    ChannelId = channel.ChannelId,
                    ChannelName = channel.ChannelName,
                    ChatUsersIds = channel.Users.Select(user => user.ChatUserId).ToList(),
                    MessagesIds = channel.Messages.Select(message => message.MessageId).ToList()
                }
            };
           
            
            
        }
    }
}
