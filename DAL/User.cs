using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class User
    {
        public long userID { get; set; }
        public string name { get; set; }
        public DTO.UserType userType { get; set; }
        public String password { get; set; }
        public bool isDeleted { get; set; }
    }
}
