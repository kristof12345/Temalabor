using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //Egy foglalást reprezentáló osztály LEHET, HOGY NEM SZÉKENKÉNT KELLENE...
    public class Reservation
    {
        //A foglaláshoz tartozó székek listája (azonosítók)
        private List<long> seatList;

        public Reservation(long fid)
        {
            FlightId = fid;
            seatList = new List<long>();
        }

        public String User { get; set; }
        public long FlightId { get; set; }
        public int SeatCount { get { return seatList.Count; } }
        public void AddSeatId(long id)
        {
            seatList.Add(id);
        }
    }
}
