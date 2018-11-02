using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //Egy foglalást reprezentáló osztály.
    public class Reservation
    {
        //A foglaláshoz tartozó székek listája (azonosítók)
        public List<long> SeatList { get; set; }

        //Konstruktor
        public Reservation(long fid)
        {
            FlightId = fid;
            SeatList = new List<long>();         
        }

        //A foglalás azonosítója
        public long ReservationId { get; set; }

        //A felhasználó
        public String User { get; set; }

        public long UserID { get; set; }

        //A járat azonosítója
        public long FlightId { get; set; }

        //A lefoglalt székek száma
        public int SeatCount { get { return SeatList.Count; } }

        //A foglalás összege
        public int Cost { get; set; }

        //A foglalás dátuma
        public DateTime Date { get; set; }

        //Szék hozzáadása a foglaláshoz
        public void AddSeatId(long id)
        {
            SeatList.Add(id);
        }
    }
}