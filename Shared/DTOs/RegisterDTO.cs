using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Shared.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")] 
        public string PasswordConfirm { get; set; } = null!;
        [DefaultValue(false)]
        public bool? UpToAuthor { get; set; }
    }
}
