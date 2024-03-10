using Microsoft.AspNetCore.Identity;

namespace UniSync.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }

    }
}
