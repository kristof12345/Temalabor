using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.Models
{
    class PageParameter
    {
        public PageParameter(User_DTO u, Flight f)
        {
            User = u;
            Flight = f;
        }
        public User_DTO User { get; set; }
        public Flight Flight { get; set; }
    }
}
