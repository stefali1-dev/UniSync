using UniSync.Application.Responses;

namespace UniSync.Application.Features.UserPhotos.Queries.GetUserPhoto
{
    public class GetUserPhotoQueryResponse : BaseResponse
    {
        public GetUserPhotoQueryResponse() : base()
        {
        }
        public Guid UserPhotoId { get; set; }
        public string UserPhotoUrl { get; set; }
    }
}