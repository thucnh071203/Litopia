using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IFriendsService
    {
        Task SendFriendRequestAsync(string senderId, string receiverId);
        Task AcceptFriendRequestAsync(string requestId, string userId);
        Task RejectFriendRequestAsync(string requestId, string userId);
        Task UnfriendAsync(string userId, string friendId);
        Task FollowAsync(string followerId, string followedId);
        Task UnfollowAsync(string followerId, string followedId);
        Task<List<FriendRequest>> GetFriendsAsync(string userId);
        Task<List<FriendRequest>> GetFollowersAsync(string userId);
        Task<List<FriendRequest>> GetFollowingAsync(string userId);
    }
}
