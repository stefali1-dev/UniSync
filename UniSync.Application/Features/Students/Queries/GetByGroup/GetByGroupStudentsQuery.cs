using MediatR;

namespace UniSync.Application.Features.Students.Queries.GetByGroup
{
    public class GetByGroupStudentsQuery : IRequest<GetByGroupStudentsQueryResponse>
    {
        public string GroupName { get; set; }
    }
}
