using UniSync.Application.Features.Channels.Queries;
using UniSync.Application.Responses;

namespace UniSync.Application.Features.Channels.Commands.CreateChannel
{
    public class CreateChannelCommandResponse : BaseResponse
    {
        public CreateChannelCommandResponse() : base()
        {
        }

        public ChannelDto Channel { get; set; } = default!;
    }
}