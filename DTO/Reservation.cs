using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //Egy foglalást reprezentáló osztály LEHET, HOGY NEM SZÉKENKÉNT KELLENE...
    public class Reservation
    {
        public Reservation(long fid, long sid, String name="Felhasznalo")
        {
            FlightId = fid;
            SeatId = sid;
            User = name;
        }

        public String User { get; set; }
        public long FlightId { get; set; }
        public long SeatId { get; set; }
    }
}
