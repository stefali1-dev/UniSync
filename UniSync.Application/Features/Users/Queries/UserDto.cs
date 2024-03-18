namespace UniSync.Application.Features.Users.Queries
{
    public class UserDto
    {
        public string? UserId { get; set; }
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Bio { get; set; }
        public Social? Social { get; set; }
        public UserCloudPhotoDto? UserPhoto { get; set; }
        public List<string>? Roles { get; set; }
    }
}
