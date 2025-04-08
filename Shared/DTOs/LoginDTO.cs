using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Please enter your Email/Username!")] 
        public string Identifier { get; set; } = null!; // Email, Username hoặc PhoneNumber (Thêm phone sau)
        [Required(ErrorMessage = "Please enter your password!")] 
        public string Password { get; set; } = null!;
    }
}
