using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //Egy foglalást reprezentáló osztály.
    public class Reservation
    {
        //A foglaláshoz tartozó székek listája (azonosítók)
        private List<long> seatList;

        //Konstruktor
        public Reservation(long fid)
        {
            FlightId = fid;
            seatList = new List<long>();
        }

        //A felhasználó
        public String User { get; set; }

        //A járat azonosítója
        public long FlightId { get; set; }

        //A lefoglalt székek száma
        public int SeatCount { get { return seatList.Count; } }

        //A foglalás összege
        public int Cost { get; set; }

        //Szék hozzáadása a foglaláshoz
        public void AddSeatId(long id)
        {
            seatList.Add(id);
        }
    }
}