using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Auth
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Incorrect email format")]
        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be atleast 6 characters")]
        [MaxLength(200, ErrorMessage = "Password cannot exceed 200 characters")]
        public string Password { get; set; } = "";
    }
}
