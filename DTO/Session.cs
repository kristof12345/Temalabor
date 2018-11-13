using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class Session
    {
        public Session() { }

        public Session(User u)
        {
            this.User = u;
        }

        //A bejelentkezés sikeressége
        public bool Success { get; set; } = true;

        //A bejelentkezéshez tartozó JWT token
        public String Token { get; set; } = "";

        //A bejelentkezéshez tartozó user
        public User User { get; set; }
    }
}
