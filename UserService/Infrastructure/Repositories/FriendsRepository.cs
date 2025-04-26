using MongoDB.Driver;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories
{
    public class FriendsRepository : IFriendsRepository
    {
        private readonly IMongoCollection<FriendRequest> _friendRequest;

        public FriendsRepository(UserDbContext context)
        {
            _friendRequest = context.FriendRequests;
        }

        // Create a friend request
        public async Task CreateAsync(FriendRequest friendRequest)
        {
            await _friendRequest.InsertOneAsync(friendRequest);
        }

        // Get a friend request by SenderId and ReceiverId (check both directions)
        public async Task<FriendRequest> GetBySenderAndReceiverAsync(string senderId, string receiverId)
        {
            return await _friendRequest.Find(fr =>
                (fr.SenderId == senderId && fr.ReceiverId == receiverId) ||
                (fr.SenderId == receiverId && fr.ReceiverId == senderId))
                .FirstOrDefaultAsync();
        }

        // Get friend request by RequestId
        public async Task<FriendRequest> GetByIdAsync(string requestId)
        {
            return await _friendRequest.Find(fr => fr.RequestId == requestId).FirstOrDefaultAsync();
        }

        // Update friend request
        public async Task UpdateAsync(FriendRequest friendRequest)
        {
            await _friendRequest.ReplaceOneAsync(fr => fr.RequestId == friendRequest.RequestId, friendRequest);
        }

        // Delete friend request
        public async Task DeleteAsync(string requestId)
        {
            await _friendRequest.DeleteOneAsync(fr => fr.RequestId == requestId);
        }

        // Get all friend requests for a user (as sender or receiver)
        public async Task<List<FriendRequest>> GetByUserIdAsync(string userId)
        {
            return await _friendRequest.Find(fr =>
                fr.SenderId == userId || fr.ReceiverId == userId)
                .ToListAsync();
        }

        // Get list of friends (accepted requests)
        public async Task<List<FriendRequest>> GetFriendsAsync(string userId)
        {
            return await _friendRequest.Find(fr =>
                (fr.SenderId == userId || fr.ReceiverId == userId) && fr.Status == "Accepted")
                .ToListAsync();
        }

        // Get followers (users who sent follow requests)
        public async Task<List<FriendRequest>> GetFollowersAsync(string userId)
        {
            return await _friendRequest.Find(fr =>
                fr.ReceiverId == userId && fr.Status == "Following")
                .ToListAsync();
        }

        // Get following (users that the user is following)
        public async Task<List<FriendRequest>> GetFollowingAsync(string userId)
        {
            return await _friendRequest.Find(fr =>
                fr.SenderId == userId && fr.Status == "Following")
                .ToListAsync();
        }
    }
}
