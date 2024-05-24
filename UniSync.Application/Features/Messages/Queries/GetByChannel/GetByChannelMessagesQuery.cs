using MediatR;

namespace UniSync.Application.Features.Messages.Queries.GetByGroup
{
    public class GetByChannelMessagesQuery : IRequest<GetByChannelMessagesQueryResponse>
    {
        public Guid ChannelId { get; set; }
    }
}
