using MongoDB.Bson;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Domain.Interfaces;

namespace UserService.Application.Services
{
    public class FriendsService : IFriendsService
    {
        private readonly IFriendsRepository _friendRepository;
        private readonly IUsersRepository _userRepository; // Giả sử bạn có repository để kiểm tra RoleId

        public FriendsService(IFriendsRepository friendRepository, IUsersRepository userRepository)
        {
            _friendRepository = friendRepository;
            _userRepository = userRepository;
        }

        // Gửi yêu cầu kết bạn
        public async Task SendFriendRequestAsync(string senderId, string receiverId)
        {
            if (senderId == receiverId)
                throw new Exception("Cannot send friend request to yourself.");

            var existingRequest = await _friendRepository.GetBySenderAndReceiverAsync(senderId, receiverId);
            if (existingRequest != null)
                throw new Exception("Friend request already exists or you are already friends.");

            var friendRequest = new FriendRequest
            {
                RequestId = ObjectId.GenerateNewId().ToString(),
                SenderId = senderId,
                ReceiverId = receiverId,
                Status = "Pending",
                RequestDate = DateTime.UtcNow
            };

            await _friendRepository.CreateAsync(friendRequest);
        }

        // Chấp nhận yêu cầu kết bạn
        public async Task AcceptFriendRequestAsync(string requestId, string userId)
        {
            var friendRequest = await _friendRepository.GetByIdAsync(requestId);
            if (friendRequest == null || friendRequest.ReceiverId != userId)
                throw new Exception("Friend request not found or you are not authorized.");

            if (friendRequest.Status != "Pending")
                throw new Exception("Friend request is not pending.");

            friendRequest.Status = "Accepted";
            await _friendRepository.UpdateAsync(friendRequest);
        }

        // Từ chối yêu cầu kết bạn
        public async Task RejectFriendRequestAsync(string requestId, string userId)
        {
            var friendRequest = await _friendRepository.GetByIdAsync(requestId);
            if (friendRequest == null || friendRequest.ReceiverId != userId)
                throw new Exception("Friend request not found or you are not authorized.");

            if (friendRequest.Status != "Pending")
                throw new Exception("Friend request is not pending.");

            await _friendRepository.DeleteAsync(requestId);
        }

        // Hủy kết bạn
        public async Task UnfriendAsync(string userId, string friendId)
        {
            var friendRequest = await _friendRepository.GetBySenderAndReceiverAsync(userId, friendId);
            if (friendRequest == null || friendRequest.Status != "Accepted")
                throw new Exception("You are not friends with this user.");

            await _friendRepository.DeleteAsync(friendRequest.RequestId);
        }

        // Theo dõi người khác (chỉ áp dụng nếu Receiver có RoleId = 6807a3224dc09155c419126c)
        public async Task FollowAsync(string followerId, string followedId)
        {
            if (followerId == followedId)
                throw new Exception("Cannot follow yourself.");

            // Kiểm tra RoleId của Receiver
            var receiver = await _userRepository.GetByIdAsync(followedId);
            if (receiver?.RoleId != "6807a3224dc09155c419126c")
                throw new Exception("This user cannot be followed.");

            var existingRequest = await _friendRepository.GetBySenderAndReceiverAsync(followerId, followedId);
            if (existingRequest != null)
                throw new Exception("You are already following or have a pending request with this user.");

            var followRequest = new FriendRequest
            {
                RequestId = ObjectId.GenerateNewId().ToString(),
                SenderId = followerId,
                ReceiverId = followedId,
                Status = "Following",
                RequestDate = DateTime.UtcNow
            };

            await _friendRepository.CreateAsync(followRequest);
        }

        // Bỏ theo dõi
        public async Task UnfollowAsync(string followerId, string followedId)
        {
            var followRequest = await _friendRepository.GetBySenderAndReceiverAsync(followerId, followedId);
            if (followRequest == null || followRequest.Status != "Following" || followRequest.SenderId != followerId)
                throw new Exception("You are not following this user.");

            await _friendRepository.DeleteAsync(followRequest.RequestId);
        }

        // Xem danh sách bạn bè
        public async Task<List<FriendRequest>> GetFriendsAsync(string userId)
        {
            return await _friendRepository.GetFriendsAsync(userId);
        }

        // Xem danh sách người theo dõi
        public async Task<List<FriendRequest>> GetFollowersAsync(string userId)
        {
            return await _friendRepository.GetFollowersAsync(userId);
        }

        // Xem danh sách đang theo dõi
        public async Task<List<FriendRequest>> GetFollowingAsync(string userId)
        {
            return await _friendRepository.GetFollowingAsync(userId);
        }
    }
}
