using UserService.DAOs;
using UserService.Models;

namespace UserService.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersDAO _usersDAO;

        public UsersRepository(UsersDAO usersDAO)
        {
            _usersDAO = usersDAO;
        }

        public Task<User?> GetByUsernameAsync(string username) => _usersDAO.GetByUsernameAsync(username);
        public Task<User?> GetByEmailAsync(string email) => _usersDAO.GetByEmailAsync(email);
        public Task<User?> GetByIdAsync(Guid userId) => _usersDAO.GetByIdAsync(userId);
        public Task<List<User>> GetAllAsync() => _usersDAO.GetAllAsync();
        public Task<User> CreateAsync(User user) => _usersDAO.CreateAsync(user);
        public Task<User> UpdateAsync(User user) => _usersDAO.UpdateAsync(user);
        public Task DeleteAsync(Guid userId) => _usersDAO.DeleteAsync(userId);
    }
}
