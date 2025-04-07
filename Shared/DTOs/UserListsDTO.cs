using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs
{
    public class UserListsDTO
    {
        public List<UserDTO> AllUsersAvailable { get; set; } = new List<UserDTO>();
        public List<UserDTO> AllUsersBanned { get; set; } = new List<UserDTO>();
        public List<UserDTO> AllAuthorsAvailable { get; set; } = new List<UserDTO>();
        public List<UserDTO> AllAuthorsBanned { get; set; } = new List<UserDTO>();
        public List<UserDTO> AllStaffAvailable { get; set; } = new List<UserDTO>();
        public List<UserDTO> AllStaffBanned { get; set; } = new List<UserDTO>();
        public List<UserDTO> AllUsersUpToAuthor { get; set; } = new List<UserDTO>();
    }
}
