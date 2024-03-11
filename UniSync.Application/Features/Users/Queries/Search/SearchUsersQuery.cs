using MediatR;
namespace UniSync.Application.Features.Users.Queries.Search
{
    public class SearchUsersQuery : IRequest<SearchUsersQueryResponse>
    {
        public string SearchValue { get; set; }
    }
}
