using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL
{
    public class Seat
    {
        public int seatID { get; set; }
        public Flight flight { get; set; }
        public bool IsReserved { get; set; }
        public String seatType { get; set; }
        public int price { get; set; }
        public int Xcord { get; set; }
        public int Ycord { get; set; }
    }
}

