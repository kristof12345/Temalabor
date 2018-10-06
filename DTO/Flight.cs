using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DTO
{
    public class Flight_DTO
    {
        //Konstruktor
        public Flight_DTO(string type)
        {
            if(type!=null) PlaneType = new PlaneType(type);
        }

        //Elsődleges kulcs az adatbázisban
        public long DatabaseId { get; set; }

        //Egyedi azonosító a kliensben (ez alapján lehet törölni és módosítani)
        public long FlightId { get; set; }

        //Dátum
        public DateTime Date { get; set; }

        //Indulás helye
        public string Departure { get; set; }

        //Érkezés helye
        public string Destination { get; set; }

        //Repülő típusa, tartalmazza a székeket
        public string PlaneType { get; set; }

        //A járat státusza (pl: Cancelled, Sceduled, Delayed)
        public string Status { get; set; }

        //ezt hagyd így
         public int NumberOfSeats { get; set; }
        
        //majd ezt valamilyen sql lekérdezéssel kell meghatározni, de most jó lesz így
        public int FreeSeats { get; set; }


        //Kiíráshoz ToStirng
        public override string ToString()
        {
            return "id: " + FlightId.ToString() + " from: " + Departure + " to: " + Destination + " type: " + PlaneType;
        }
    }
}
