using MediatR;

namespace UniSync.Application.Features.Users.Queries.GetById
{
    public class GetByIdUserQuery : IRequest<GetByIdUserQueryResponse>
    {
        public string UserId { get; set; }
    }
}
