using Inventory.Models;
using Inventory.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Store
{
    public class UserStore
    {
        private readonly UserService _userService = null!;
        public static bool IsLoggedIn = false;

        public UserStore()
        {
            _userService = new();
        }

        public async Task<bool> Login(User user)
        {
          return await _userService.Login(user);
        }
    }
}
