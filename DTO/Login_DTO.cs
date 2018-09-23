using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //Egy felhasználó bejelentkezési kérelme
    public class Login_DTO
    {
        public Login_DTO(User_DTO user)
        {
            User = user;
        }
        public User_DTO User { get; set; }
    }
}
