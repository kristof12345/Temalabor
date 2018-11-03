using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //A foglalás részletei
    public class ReservationDetails
    {
        public String Departure="";

        public String Destination="";

        //Stb...

        public override String ToString()
        {
            return "Fligth from " + Departure + " to " + Destination;
        }
    }

    //Egy foglalást reprezentáló osztály.
    public class Reservation
    {
        //A foglalás részletei
        private ReservationDetails details = new ReservationDetails();

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

        public String SeatCountString { get { return SeatCount + " seats"; } }

        //A foglalás összege
        public int Cost { get; set; }

        //A foglalás dátuma
        public DateTime Date { get; set; }

        //A foglalás részletei
        public String Details { get { return details.ToString(); } }

        //Szék hozzáadása a foglaláshoz
        public void AddSeatId(long id)
        {
            SeatList.Add(id);
        }
    }
}