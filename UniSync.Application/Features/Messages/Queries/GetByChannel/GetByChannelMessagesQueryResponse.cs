using UniSync.Application.Responses;

namespace UniSync.Application.Features.Messages.Queries.GetByGroup
{
    public class GetByChannelMessagesQueryResponse : BaseResponse
    {
        public GetByChannelMessagesQueryResponse() : base() 
        {

        }
        public List<MessageDto>? Messages { get; set; }
    }
}
