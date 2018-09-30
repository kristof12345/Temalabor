using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    class Reservation
    {
        public long reservationID { get; set; }
        public String user { get; set; }
        public Flight flight { get; set; }
        public Seat seat { get; set; }
    }
}
