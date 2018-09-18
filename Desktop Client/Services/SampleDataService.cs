using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Desktop_Client.Models;

namespace Desktop_Client.Services
{
    public static class DataService
    {
        private static ObservableCollection<SampleOrder> data = new ObservableCollection<SampleOrder>
          {
                new SampleOrder(5)
                {
                    FlightId = 111,
                    Date = new DateTime(2017, 05, 24),
                    Departure = "London",
                    Destination = "New York",
                    PlaneType = "Airbus A380",
                    Status = "Cancelled",
                },
        };

        private static IEnumerable<SampleOrder> AllOrders()
        {
            return data;
        }

        public static void AddFlight(int id)
        {
            var temp = new SampleOrder(5)
            {
                FlightId = id,
                Date = new DateTime(2018, 05, 24),
                Departure = "London",
                Destination = "New York",
                PlaneType = "Airbus A370",
                Status = "Arrived",
            };

            data.Add(temp);
        }

        public static void Reserve(int flightid, int seatid)
        {
            data[flightid].ReserveSeat(seatid);
        }

        public static ObservableCollection<SampleOrder> GetGridData()
        {
            return new ObservableCollection<SampleOrder>(AllOrders());
        }
    }
}
