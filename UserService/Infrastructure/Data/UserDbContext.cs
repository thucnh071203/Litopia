using MongoDB.Driver;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Data
{
    public class UserDbContext
    {
        private readonly IMongoDatabase _database;

        public UserDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("DefaultConnection"));
            _database = client.GetDatabase("Litopia_UserServiceDB");
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>("users");
        public IMongoCollection<Role> Roles => _database.GetCollection<Role>("roles");
        public IMongoCollection<FriendRequest> FriendRequests => _database.GetCollection<FriendRequest>("friend-request");
    }
}
