using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class ReservationDetails
    {
        public long reservationDetailsID { get; set; }
        public long reservationID { get; set; }
        public String departure { get; set; }
        public String destination { get; set; }
    }
}
