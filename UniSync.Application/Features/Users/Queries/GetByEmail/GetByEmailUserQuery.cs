using MediatR;

namespace UniSync.Application.Features.Users.Queries.GetByEmail
{
    public class GetByEmailUserQuery : IRequest<GetByEmailUserQueryReponse>
    {
        public string Email { get; set; }
    }
}
