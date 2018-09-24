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

    public class User_DTO
    {
        private UserType type;
        private String password;

        public User_DTO(string name, string pass, UserType t = UserType.Customer)
        {
            Name = name;
            password = pass;
            type = t;
        }

        public string Name { get; set; }

        public UserType UserType { get; set; }

        //Ez nem túl biztonságos...
        public String GetPassword()
        {
            return password;
        }

        //Egyéb dolgok
    }
}
