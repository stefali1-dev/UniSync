using System.ComponentModel.DataAnnotations;

namespace UniSync.Application.Models.Identity
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "Registration Id is required")]
        public string? RegistrationId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
