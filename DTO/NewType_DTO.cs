using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    public class NewType_DTO
    {
        public NewType_DTO(String n, List<Seat> l)
        {
            Name = n;
            seats = l;
        }
        public String Name;
        public List<Seat> seats;
    }
}
