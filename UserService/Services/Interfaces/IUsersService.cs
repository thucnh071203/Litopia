using Shared.DTOs;
using UserService.Models;

namespace UserService.Services.Interfaces
{
    public interface IUsersService
    {
        Task<string?> LoginAsync(LoginDTO loginDto);
        Task<User?> RegisterAsync(RegisterDTO registerDto);
        Task<User?> GetByIdAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllUsersAvailableAsync();
        Task<User?> UpdateAsync(Guid userId, UserDTO userDto);
        Task DeleteAsync(Guid userId);
    }
}
