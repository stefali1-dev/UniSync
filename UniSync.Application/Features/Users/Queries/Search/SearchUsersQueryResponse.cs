using UniSync.Application.Responses;
namespace UniSync.Application.Features.Users.Queries.Search
{
    public class SearchUsersQueryResponse : BaseResponse
    {
        public UserSearchDto[] Users { get; set; } = [];
    }
}
