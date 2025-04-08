using Shared.DTOs;

namespace Client.ViewModels
{
    public class UserListViewModel
    {
        public List<UserDTO> Users { get; set; }
        public string FilterType { get; set; }
        public string ErrorMessage { get; set; }
    }
}
