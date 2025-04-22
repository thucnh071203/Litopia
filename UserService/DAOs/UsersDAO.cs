using Microsoft.EntityFrameworkCore;
using UserService.Models;

namespace UserService.DAOs
{
    public class UsersDAO : SingletonBase<UsersDAO>
    {   
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username && (u.IsDeleted != true));
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Email == email && (u.IsDeleted != true));
        }

        public async Task<User?> GetByIdAsync(Guid userId)
        {
            return await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public IQueryable<User> GetUsersQueryable()
        {
            return _context.Users.AsNoTracking();
        }

        public async Task<List<User>> GetAllUsersAvailableAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Where(u => u.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(Guid userId)
        {
            var user = await GetByIdAsync(userId);
            if (user != null)
            {
                user.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
