using UniSync.Application.Features.Users.Queries;
using UniSync.Application.Responses;

namespace UniSync.Application.Features.Students.Queries.GetByGroup
{
    public class GetByGroupStudentsQueryResponse : BaseResponse
    {
        public GetByGroupStudentsQueryResponse() : base() 
        {

        }
        public List<ChatDto>? Students { get; set; }
    }
}
