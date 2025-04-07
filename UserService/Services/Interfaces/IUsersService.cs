using Shared.DTOs;
using UserService.Models;

namespace UserService.Services.Interfaces
{
    public interface IUsersService
    {
        Task<string?> LoginAsync(LoginDTO loginDto);
        Task<User?> RegisterAsync(RegisterDTO registerDto);
        Task<User?> GetByIdAsync(Guid userId);
        Task<User> CreateAsync(User user);
        IQueryable<User> GetUsersQueryable();
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllUsersAvailableAsync();
        Task<List<User>> GetAllBannedUsersAsync();
        Task<User?> UpdateAsync(Guid userId, User user);
        Task<User?> RestoreAsync(Guid userId);
        Task DeleteAsync(Guid userId);
    }
}
