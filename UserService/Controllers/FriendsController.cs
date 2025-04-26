using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Interfaces;

namespace UserService.Controllers
{
    [Route("api/friends")]
    [ApiController]
    public class FriendsController : ControllerBase
    {
        private readonly IFriendsService _friendService;

        public FriendsController(IFriendsService friendService)
        {
            _friendService = friendService;
        }

        [HttpPost("request")]
        public async Task<IActionResult> SendFriendRequest([FromBody] FriendRequestDto request)
        {
            try
            {
                await _friendService.SendFriendRequestAsync(request.SenderId, request.ReceiverId);
                return Ok("Friend request sent successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Chấp nhận yêu cầu kết bạn
        [HttpPut("accept/{requestId}")]
        public async Task<IActionResult> AcceptFriendRequest(string requestId, [FromBody] UserIdDto user)
        {
            try
            {
                await _friendService.AcceptFriendRequestAsync(requestId, user.UserId);
                return Ok("Friend request accepted.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Từ chối yêu cầu kết bạn
        [HttpDelete("reject/{requestId}")]
        public async Task<IActionResult> RejectFriendRequest(string requestId, [FromBody] UserIdDto user)
        {
            try
            {
                await _friendService.RejectFriendRequestAsync(requestId, user.UserId);
                return Ok("Friend request rejected.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Hủy kết bạn
        [HttpDelete("unfriend")]
        public async Task<IActionResult> Unfriend([FromBody] FriendRequestDto request)
        {
            try
            {
                await _friendService.UnfriendAsync(request.SenderId, request.ReceiverId);
                return Ok("Unfriended successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Theo dõi người khác
        [HttpPost("follow")]
        public async Task<IActionResult> Follow([FromBody] FriendRequestDto request)
        {
            try
            {
                await _friendService.FollowAsync(request.SenderId, request.ReceiverId);
                return Ok("Followed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Bỏ theo dõi
        [HttpDelete("unfollow")]
        public async Task<IActionResult> Unfollow([FromBody] FriendRequestDto request)
        {
            try
            {
                await _friendService.UnfollowAsync(request.SenderId, request.ReceiverId);
                return Ok("Unfollowed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Xem danh sách bạn bè
        [HttpGet("friends/{userId}")]
        public async Task<IActionResult> GetFriends(string userId)
        {
            try
            {
                var friends = await _friendService.GetFriendsAsync(userId);
                return Ok(friends);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Xem danh sách người theo dõi
        [HttpGet("followers/{userId}")]
        public async Task<IActionResult> GetFollowers(string userId)
        {
            try
            {
                var followers = await _friendService.GetFollowersAsync(userId);
                return Ok(followers);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }

        // Xem danh sách đang theo dõi
        [HttpGet("following/{userId}")]
        public async Task<IActionResult> GetFollowing(string userId)
        {
            try
            {
                var following = await _friendService.GetFollowingAsync(userId);
                return Ok(following);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    // DTO để nhận dữ liệu từ request body
    public class FriendRequestDto
    {
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
    }

    public class UserIdDto
    {
        public string UserId { get; set; }
    }
}

