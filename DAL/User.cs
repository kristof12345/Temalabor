using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public enum UserType
    {
        Customer,
        Administrator
    }

    public class User
    {
        public long userID { get; set; }
        public string name { get; set; }
        public UserType userType { get; set; }
        public String password { get; set; }
    }
}
