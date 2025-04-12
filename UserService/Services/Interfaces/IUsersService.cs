using Shared.DTOs;
using UserService.Models;

namespace UserService.Services.Interfaces
{
    public interface IUsersService
    {
        Task<LoginResponseDTO> LoginAsync(LoginDTO loginDto);
        Task<LoginResponseDTO> LoginWithGoogleAsync(LoginGoogleDTO request);
        Task<User?> RegisterAsync(RegisterDTO registerDto);
        Task<User?> GetByIdAsync(Guid userId);
        Task<User?> GetByEmailAsync(string email);
        Task<User> CreateAsync(User user);
        IQueryable<User> GetUsersQueryable();
        Task<List<User>> GetAllUsersAvailableAsync();
        Task<User?> UpdateAsync(Guid userId, User user);
        Task<User?> RestoreAsync(Guid userId);
        Task DeleteAsync(Guid userId);
    }
}
