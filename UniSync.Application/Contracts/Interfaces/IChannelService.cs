using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Application.Features.Channels;
using UniSync.Application.Features.Evaluation;

namespace UniSync.Application.Contracts.Interfaces
{
    public interface IChannelService
    {
        public Task AddChannel(ChannelCreationDto channelCreationDto);
    }
}
