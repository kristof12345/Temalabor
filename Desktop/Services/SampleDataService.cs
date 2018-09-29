using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Desktop.Models;
using DTO;

namespace Desktop.Services
{
    public static class DataService
    {
            private static ObservableCollection<Flight> data = new ObservableCollection<Flight>
            {
                //Alap járat
                new Flight(9) {FlightId = 0, Date = new DateTime(2017, 05, 24), Departure = "London", Destination = "New York", PlaneType = "Airbus A380", Status = "Cancelled",},
            };

        //Teljes repülőjárat adatbázis
        public static ObservableCollection<Flight> GetGridData()
        {
            return data;
        }

        //Foglalás adatbázis
        public static ObservableCollection<Reservation> GetReservations()
        {
            throw new NotImplementedException(); //TODO: implement
        }

        //Járat hozzáadása
        public static void AddFlight(int id, DateTime date, String dep = "London", String dest="New York", String type = "Airbus A370")
        {
            var temp = new Flight(5)
            {
                FlightId = id,
                Date = date,
                Departure = dep,
                Destination = dest,
                PlaneType = type,
                Status = "Sceduled",
            };
            //Hozzáadás a memóriabeli adatbázishoz
            data.Add(temp);

            //Http kérés kiadása
            HttpService.PostAddFlightAsync(temp.ToDTO());
        }

        //Járat törlése
        public static void DeleteFlight(Flight f)
        {
            //Törlés a memóriabeli adatbázisból
            data.Remove(f);

            //Http kérés kiadása
            HttpService.PostDeleteFlightAsync(new DeleteFlight_DTO(f.FlightId));
        }

        //Foglalás hozzáadása
        public static void Reserve(int flightid, int seatid)
        {
            data[flightid].ReserveSeat(seatid);
        }
    }
}
