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
                new Flight(9)
                {
                    FlightId = 0,
                    Date = new DateTime(2017, 05, 24),
                    Departure = "London",
                    Destination = "New York",
                    PlaneType = "Airbus A380",
                    Status = "Cancelled",
                },
            };

        internal static ObservableCollection<Reservation> GetReservations()
        {
            throw new NotImplementedException();
        }

        private static IEnumerable<Flight> AllFlights()
        {
            return data;
        }

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

            data.Add(temp);
        }

        public static void Reserve(int flightid, int seatid)
        {
            data[flightid].ReserveSeat(seatid);
        }

        public static ObservableCollection<Flight> GetGridData()
        {
            return data;
        }
    }
}
