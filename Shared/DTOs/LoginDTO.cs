using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class LoginDTO
    {
        [Required] 
        public string Identifier { get; set; } = null!; // Email, Username hoặc PhoneNumber (Thêm phone sau)
        [Required] 
        public string Password { get; set; } = null!;
    }
}
