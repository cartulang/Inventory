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
        public static bool IsLoggedIn { get; private set; } = false;
        public static bool IsAdmin { get; private set; } = false;
        public static string UserName { get; private set; } = null!;

        public UserStore()
        {
            _userService = new();
        }

        public async Task<bool> Login(User user)
        {
          var userLoggingIn = await _userService.Login(user);

            if(userLoggingIn == null)
            {
                IsLoggedIn = false;
                IsAdmin = false;
                UserName = "";
                return false;
            }

            UserName = userLoggingIn.Username;
            IsAdmin = userLoggingIn.Role == "admin" ? true : false;
            IsLoggedIn = true;
            return true;
        }

        public void Logout()
        {
            IsLoggedIn = false;
            IsAdmin = false;
            UserName = "";
        }
    }
}
