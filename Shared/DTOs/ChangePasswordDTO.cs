using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class ChangePasswordDTO
    {
        [Required]
        [Length(6, 64, ErrorMessage = "Password must be at least 6 characters!")]
        public string CurrentPassword { get; set; }
        [Required]
        [Length(6, 64, ErrorMessage = "Password must be at least 6 characters!")]
        public string NewPassword { get; set; }
        [Required]
        [Length(6, 64, ErrorMessage = "Password must be at least 6 characters!")]
        [Compare(nameof(NewPassword), ErrorMessage = "Confirm password does not match!")]
        public string ConfirmPassword { get; set; }
    }
}
