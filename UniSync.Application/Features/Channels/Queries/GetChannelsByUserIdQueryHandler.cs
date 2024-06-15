using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Application.Features.Messages.Queries.GetByGroup;
using UniSync.Application.Features.Messages;
using UniSync.Application.Persistence;

namespace UniSync.Application.Features.Channels.Queries
{
    public class GetChannelsByUserIdQueryHandler : IRequestHandler<GetChannelsByUserIdQuery, GetChannelsByUserIdQueryResponse>
    {
        private readonly IChannelRepository channelRepository;

        public GetChannelsByUserIdQueryHandler(IChannelRepository channelRepository)
        {
            this.channelRepository = channelRepository;
        }

        public async Task<GetChannelsByUserIdQueryResponse> Handle(GetChannelsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await channelRepository.GetChannelsByUserIdAsync(request.UserId);

            if (!result.IsSuccess)
                return new GetChannelsByUserIdQueryResponse { Success = false, Message = result.Error };

            GetChannelsByUserIdQueryResponse response = new();

            response.Channels = result.Value.Select(u => new ChannelDto
            {
                ChannelId = u.ChannelId,
                ChannelName = u.ChannelName,
                ChatUsersIds = u.Users.Select(user => user.ChatUserId.ToString()).ToList(),
                MessagesIds = u.Messages.Select(m => m.MessageId.ToString()).ToList()

            }).ToList();

            return response;
        }
    }
}
