using UniSync.Application.Responses;

namespace UniSync.Application.Features.Users.Queries.GetById
{
    public class GetByIdUserQueryResponse : BaseResponse
    {
        public GetByIdUserQueryResponse() : base()
        {
        }

        public UserDto User { get; set; }
    }
}
