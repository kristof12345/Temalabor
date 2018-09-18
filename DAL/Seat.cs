using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Seat
    {
        public int seatID { get; set; }
        //public KeyValuePair<String, int> type = new KeyValuePair<String, int>("Abc", 5){ get; set; } 
        public bool IsReserved { get; set; }
    }
}
