using Shared.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IUsersService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO loginDto);
        Task<LoginResponseDTO> LoginWithGoogleAsync(LoginGoogleDTO request);
        Task<User?> RegisterAsync(RegisterDTO registerDto);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(string id, User user);
        Task DeleteAsync(string id);
        Task RestoreAsync(string id);
        Task<string> GenerateOtpAsync(string email);
        Task<bool> ConfirmOtpAsync(string email, string otp);
    }
}
