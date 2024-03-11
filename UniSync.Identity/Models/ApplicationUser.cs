using Microsoft.AspNetCore.Identity;

namespace UniSync.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Bio { get; set; }
        public string? GitHub { get; set; }
        public string? LinkedIn { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public void UpdateData(string name, string email)
        {
            Name = name;
            Email = email;
        }

    }
}
