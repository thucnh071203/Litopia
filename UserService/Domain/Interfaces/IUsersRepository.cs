using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces
{
    public interface IUsersRepository
    {
        Task<User?> GetByUsernameAsync(string username);
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetAllAsync();
        Task<User> GetByIdAsync(string id);
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(string id, User user);
        Task DeleteAsync(string id);
        Task RestoreAsync(string id);
    }
}
