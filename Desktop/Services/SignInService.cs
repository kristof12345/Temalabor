using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Services
{
    class SignInService
    {
        public static User_DTO User { get; private set; }
        public static async Task<bool> SignInAsync(User_DTO u)
        {
            if (await HttpService.PostLoginAsync(u.Name, u.Password))
            {
                User = u;
                return true;
            }
            return false;
        }
        public static void SignOut()
        {
            User = null;
        }
    }
}
