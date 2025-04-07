using UserService.DAOs;
using UserService.Models;
using UserService.Repositories.Interfaces;

namespace UserService.Repositories.Implement
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UsersDAO _usersDAO;

        public UsersRepository(UsersDAO usersDAO)
        {
            _usersDAO = usersDAO;
        }

        public async Task<User?> GetByUsernameAsync(string username) => await _usersDAO.GetByUsernameAsync(username);
        public async Task<User?> GetByEmailAsync(string email) => await _usersDAO.GetByEmailAsync(email);
        public async Task<User?> GetByIdAsync(Guid userId) => await _usersDAO.GetByIdAsync(userId);
        public IQueryable<User> GetUsersQueryable()
        {
            return _usersDAO.GetUsersQueryable();
        }
        public async Task<List<User>> GetAllUsersAsync() => await _usersDAO.GetAllUsersAsync();
        public async Task<List<User>> GetAllUsersAvailableAsync() => await _usersDAO.GetAllUsersAvailableAsync();
        public async Task<List<User>> GetAllBannedUsersAsync() => await _usersDAO.GetAllBannedUsersAsync();
        public async Task<User> CreateAsync(User user) => await _usersDAO.CreateAsync(user);
        public async Task<User> UpdateAsync(User user) => await _usersDAO.UpdateAsync(user);
        public async Task DeleteAsync(Guid userId) => await _usersDAO.DeleteAsync(userId);
    }
}
