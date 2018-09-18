using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Desktop_Client.Models;

namespace Desktop_Client.Services
{
    public static class DataService
    {
        private static ObservableCollection<Flight> data = new ObservableCollection<Flight>
          {
                new Flight(5)
                {
                    FlightId = 111,
                    Date = new DateTime(2017, 05, 24),
                    Departure = "London",
                    Destination = "New York",
                    PlaneType = "Airbus A380",
                    Status = "Cancelled",
                },
        };

        private static IEnumerable<Flight> AllFlights()
        {
            return data;
        }

        public static void AddFlight(int id)
        {
            var temp = new Flight(5)
            {
                FlightId = id,
                Date = new DateTime(2018, 05, 24),
                Departure = "London",
                Destination = "New York",
                PlaneType = "Airbus A370",
                Status = "Arrived",
            };
            
            Debug.WriteLine(data.Count);
            data.Add(temp);
        }

        public static void Reserve(int flightid, int seatid)
        {
            data[flightid].ReserveSeat(seatid);
        }

        public static ObservableCollection<Flight> GetGridData()
        {
            return new ObservableCollection<Flight>(AllFlights());
        }
    }
}
