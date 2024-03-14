using UniSync.Application.Persistence;
using MediatR;
namespace UniSync.Application.Features.Users.Queries.Search
{
    public class SearchUsersQueryHandler : IRequestHandler<SearchUsersQuery, SearchUsersQueryResponse>
    {
        private readonly IUserManager userManager;
        private readonly IUserPhotoRepository userPhotoRepository;

        public SearchUsersQueryHandler(IUserManager userManager, IUserPhotoRepository userPhotoRepository)
        {
            this.userManager = userManager;
            this.userPhotoRepository = userPhotoRepository;
        }

        public async Task<SearchUsersQueryResponse> Handle(SearchUsersQuery request, CancellationToken cancellationToken)
        {
            var allUsers = await userManager.GetAllAsync();
            if (!allUsers.IsSuccess)
            {
                return new SearchUsersQueryResponse { Success = false, Message = allUsers.Error };
            }

            var users = allUsers.Value.Where(u =>
                    (!string.IsNullOrWhiteSpace(u.FirstName) && u.FirstName.ToLower().Contains(request.SearchValue.ToLower())) ||
                    (!string.IsNullOrWhiteSpace(u.LastName) && u.LastName.ToLower().Contains(request.SearchValue.ToLower()))
                ).ToArray();

            return new SearchUsersQueryResponse
            {
                Success = true,
                Users = users.Select(u => new UserSearchDto
                {
                    UserId = u.UserId,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    UserPhoto = !string.IsNullOrWhiteSpace(u.UserId) &&
                                userPhotoRepository.GetUserPhotoByUserIdAsync(u.UserId).Result.IsSuccess
                        ? new UserCloudPhotoDto
                        {
                            UserPhotoId = userPhotoRepository.GetUserPhotoByUserIdAsync(u.UserId).Result.Value.UserPhotoId,
                            PhotoUrl = userPhotoRepository.GetUserPhotoByUserIdAsync(u.UserId).Result.Value.PhotoUrl
                        }
                        : null
                }).Take(25).ToArray()
            };
        }
    }
}
