using UserService.Models;

namespace UserService.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid userId);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetAllUsersAvailableAsync();
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(Guid userId);
    }
}
