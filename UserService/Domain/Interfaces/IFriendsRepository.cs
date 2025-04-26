using UserService.Domain.Entities;

namespace UserService.Domain.Interfaces
{
    public interface IFriendsRepository
    {
        Task CreateAsync(FriendRequest friendRequest);
        Task<FriendRequest> GetBySenderAndReceiverAsync(string senderId, string receiverId);
        Task<FriendRequest> GetByIdAsync(string requestId);
        Task UpdateAsync(FriendRequest friendRequest);
        Task DeleteAsync(string requestId);
        Task<List<FriendRequest>> GetByUserIdAsync(string userId);
        Task<List<FriendRequest>> GetFriendsAsync(string userId);
        Task<List<FriendRequest>> GetFollowersAsync(string userId);
        Task<List<FriendRequest>> GetFollowingAsync(string userId);
    }
}
