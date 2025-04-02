namespace UserService.DTOs
{
    public class UserDTO
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }
        public string? Bio { get; set; }
        public string RoleName { get; set; } = null!;
    }
}
