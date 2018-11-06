using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class SeatsOnFlight
    {
        public long seatsOnFlightID { get; set; }
        public long flightID { get; set; }
        public Flight Flight { get; set; }
        public long seatID { get; set; }
        public Seat Seat { get; set; }
        public bool isReserved { get; set; }
    }
}
