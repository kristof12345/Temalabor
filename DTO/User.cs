using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public enum UserType
    {
        Customer,
        Administrator
    }

    public class User
    {
        public User(string name, string pass, UserType t = UserType.Customer)
        {
            Name = name;
            Password = pass;
            UserType = t;
        }

        public long UserId { get; set; }

        public string Name { get; set; }

        public UserType UserType { get; set; }

        //Ez nem túl biztonságos...
        public String Password { get; set; }

        //Egyéb dolgok
    }
}
