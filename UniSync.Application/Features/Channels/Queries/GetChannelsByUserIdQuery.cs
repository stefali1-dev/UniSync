using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Application.Features.Messages.Queries.GetByGroup;

namespace UniSync.Application.Features.Channels.Queries
{
    public class GetChannelsByUserIdQuery : IRequest<GetChannelsByUserIdQueryResponse>
    {
        public Guid UserId{ get; set; }
    }
}
