using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModels
{
    public class UsersViewModel
    {
        public List<UserDTO> Users { get; set; }
        public string FilterType { get; set; }
        public string ErrorMessage { get; set; }
        public string SearchString { get; set; }
    }
}
