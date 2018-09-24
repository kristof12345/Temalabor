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
        public static void SignIn(User_DTO u)
        {
            User = u;
        }
        public static void SignOut()
        {
            User = null;
        }
    }
}
