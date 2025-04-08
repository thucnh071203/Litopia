using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class LoginResponseDTO
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
        public string ErrorMessage { get; set; } // Optional: để báo lỗi nếu thất bại
    }
}
