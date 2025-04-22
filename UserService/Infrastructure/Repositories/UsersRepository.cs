using MongoDB.Bson;
using MongoDB.Driver;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly IMongoCollection<User> _users;

        public UsersRepository(UserDbContext context)
        {
            _users = context.Users;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _users.Find(u => u.UserId == id && u.IsDeleted != true).SingleOrDefaultAsync();
        }

        public async Task<User> CreateAsync(User user)
        {
            user.UserId = ObjectId.GenerateNewId().ToString();
            user.CreatedDate = DateTime.UtcNow;
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> UpdateAsync(string id, User user)
        {
            user.UserId = id;
            user.UpdatedDate = DateTime.UtcNow;
            await _users.ReplaceOneAsync(u => u.UserId == id && u.IsDeleted != true, user);
            return user;
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<User>.Filter.Where(u => u.UserId == id && u.IsDeleted != true);
            var update = Builders<User>.Update.Set(u => u.IsDeleted, true);

            await _users.UpdateOneAsync(filter, update);
        }

        public async Task RestoreAsync(string id)
        {
            var filter = Builders<User>.Filter.Where(u => u.UserId == id && u.IsDeleted == true);
            var update = Builders<User>.Update.Set(u => u.IsDeleted, false);

            await _users.UpdateOneAsync(filter, update);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username && u.IsDeleted != true).SingleOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email && u.IsDeleted != true).SingleOrDefaultAsync();
        }
    }
}
