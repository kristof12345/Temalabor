using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class Reservation
    {
        public long reservationID { get; set; }
        public String user { get; set; }
        public long userID { get; set; }
        public long flightID { get; set; }
        public DateTime date { get; set; }
        public int cost { get; set; }
    }
}
