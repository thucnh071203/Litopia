﻿namespace UserService.DTOs
{
    public class RegisterDTO
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;   
    }
}
