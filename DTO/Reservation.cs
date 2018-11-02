using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //Egy foglalást reprezentáló osztály.
    public class Reservation
    {
        //A foglaláshoz tartozó székek listája (azonosítók)
        public List<long> seatList { get; set; }

        //Konstruktor
        public Reservation(long fid)
        {
            FlightId = fid;
            seatList = new List<long>();
        }

        //A foglalás azonosítója
        public long ReservationID { get; set; }

        //A felhasználó
        public String User { get; set; }

        public long UserID { get; set; }

        //A járat azonosítója
        public long FlightId { get; set; }

        //A lefoglalt székek száma
        public int SeatCount { get { return seatList.Count; } }

        //A foglalás összege
        public int Cost { get; set; }

        //A foglalás dátuma
        public DateTime Date { get; set; }

        //Szék hozzáadása a foglaláshoz
        public void AddSeatId(long id)
        {
            seatList.Add(id);
        }
    }
}