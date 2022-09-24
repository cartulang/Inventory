using Inventory.DbContexts;
using Inventory.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Inventory.Services
{
    public class UserService
    {
        private readonly DeviceInventoryContext _context = null!;

        public UserService()
        {
            _context = new();
        }


        public async Task<bool> Login(User user)
        {
            try
            {
                if(user.Username == null || user.Password == null)
                {
                    MessageBox.Show("All fields are required.");
                    return false;
                }

                var userMatch = await _context.Users.FirstAsync(u => u.Username == user.Username);

                if(userMatch == null)
                {
                    MessageBox.Show("Username or password does not exist.");
                    return false;
                }

                if(userMatch.Password != user.Password)
                {
                    MessageBox.Show("Username or Password does not match.");
                    return false;
                }

                return true;
            }

            catch(Exception)
            {
                MessageBox.Show("Error while logging in.");
                return false;
            }
        }
    }
}
