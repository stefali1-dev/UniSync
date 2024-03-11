using UniSync.Application.Responses;

namespace UniSync.Application.Features.Users.Queries.GetByEmail
{
    public class GetByEmailUserQueryReponse : BaseResponse
    {
        public GetByEmailUserQueryReponse() : base()
        {
            
        }
        public UserDto User { get; set; }
    }
}