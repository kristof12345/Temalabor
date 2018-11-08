using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ReservationSeat
    {
        public long reservationSeatID { get; set; }
        public long reservationID { get; set; }        
        public long seatID { get; set; }
    }
}
