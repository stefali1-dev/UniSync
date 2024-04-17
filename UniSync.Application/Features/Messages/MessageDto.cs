using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniSync.Domain.Entities;

namespace UniSync.Application.Features.Messages
{
    public class MessageDto
    {
        public Guid MessageId { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string ChannelName { get; set; }
    }
}
