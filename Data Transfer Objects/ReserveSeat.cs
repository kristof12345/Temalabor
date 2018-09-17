using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Transfer_Objects
{
    class ReserveSeat
    {
        private int flightId;
        private int seatId;

        public int FlightId { get => flightId; set => flightId = value; }
        public int SeatId { get => seatId; set => seatId = value; }
    }
}
