using MediatR;
using UniSync.Application.Persistence;

namespace UniSync.Application.Features.Messages.Queries.GetByGroup
{
    public class GetByChannelMessagesQueryHandler : IRequestHandler<GetByChannelMessagesQuery, GetByChannelMessagesQueryResponse>
    {
        private readonly IMessageRepository messageRepository;

        public GetByChannelMessagesQueryHandler(IMessageRepository messageRepository)
        {
            this.messageRepository = messageRepository;
        }
        public async Task<GetByChannelMessagesQueryResponse> Handle(GetByChannelMessagesQuery request, CancellationToken cancellationToken)
        {
            var result = await messageRepository.GetMessagesByChannelAsync(request.ChannelId);
            if (!result.IsSuccess)
                return new GetByChannelMessagesQueryResponse { Success = false, Message = result.Error };

            GetByChannelMessagesQueryResponse response = new();

            response.Messages = result.Value.Select(u => new MessageDto
            {
                MessageId = u.MessageId,
                Content = u.Content,
                ChatUserId = u.ChatUserId,
                Timestamp = u.Timestamp,
                ChannelId = u.ChannelId
            }).ToList();

            return response;
        }
    }
}
