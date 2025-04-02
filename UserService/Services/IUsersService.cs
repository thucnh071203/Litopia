using System.Threading.Tasks;
using UserService.DTOs;
using UserService.Models;

namespace UserService.Services
{
    public interface IUsersService
    {
        Task<string> LoginAsync(LoginDTO loginDto);
        Task<User> RegisterAsync(RegisterDTO registerDto);
        Task<User> GetByIdAsync(Guid userId);
        Task<List<User>> GetAllAsync();
        Task<User> UpdateAsync(Guid userId, User user);
        Task BanUserAsync(Guid userId);
    }
}
