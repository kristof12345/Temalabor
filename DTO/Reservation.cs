using System;
using System.Collections.Generic;
using System.Text;

namespace DTO
{
    //A foglalás részletei
    public class ReservationDetails
    {
        //Ezeket az adatbázisban ki kellene tölteni...
        public String Departure = "";

        public String Destination = "";

        public DateTime TravelDateTime = DateTime.Today;

        public String TravelDate { get { return TravelDateTime.ToShortDateString(); } }

        //Stb...

        //Egyéb: pl.: a jegy, a beszállókapu sorszáma...

        public String DetailsString
        {
            get
            {
                return "Fligth from " + Departure + " to " + Destination;
            }
        }
    }

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
        public String UserName { get; set; }

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
        public ReservationDetails Details { get; set; } = new ReservationDetails();

        //Szék hozzáadása a foglaláshoz
        public void AddSeatId(long id)
        {
            SeatList.Add(id);
        }
    }
}