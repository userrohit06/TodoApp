using System.ComponentModel.DataAnnotations;

namespace backend.DTO.Auth
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(150, ErrorMessage = "Email cannot exceed 150 characters")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be atleast 6 characters")]
        [MaxLength(200, ErrorMessage = "Password cannot exceed 200 characters")]
        public string Password { get; set; } = "";
    }
}
