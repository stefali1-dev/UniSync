using System.ComponentModel.DataAnnotations;
using API.Entities;

namespace API.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "Registration Id is required.")]
    public string RegistrationId { get; set; }
    
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email format.")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "Password is required.")]
    //[MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
    //[RegularExpression(@"^(?=.*[a-z])(?=.*\d)[A-Za-z\d]{6,}$", ErrorMessage = "Password must include at least one lowercase letter and one number.")]
    public string Password { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
