using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //Egy felhasználó bejelentkezési kérelme
    public class Login_DTO
    {
        public Login_DTO(User user)
        {
            User = user;
        }
        public User User { get; set; }
    }
}
