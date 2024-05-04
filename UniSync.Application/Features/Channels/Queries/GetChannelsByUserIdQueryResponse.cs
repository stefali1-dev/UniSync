using UniSync.Application.Responses;

namespace UniSync.Application.Features.Channels.Queries
{
    public class GetChannelsByUserIdQueryResponse : BaseResponse
    {
        public GetChannelsByUserIdQueryResponse() : base() { }

        public List<ChannelDto>? Channels { get; set; }
    }
}
