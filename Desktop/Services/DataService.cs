using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Desktop.Models;
using DTO;

namespace Desktop.Services
{
    public static class DataService
    {
        private static ObservableCollection<Flight> flightList;
        

        private static ObservableCollection<Reservation> reservationList = new ObservableCollection<Reservation>();
        //private static Object syncObject = new Object();

        public static event EventHandler ChangedEvent;

        public static ObservableCollection<Flight> FlightList
        {
            get
            {
                if (flightList == null)
                {
                    InitializeList();
                }
                return flightList;
            }
            set
            {
                flightList = value;
            }
        }

        private static async void InitializeList()
        {
            List<Flight_DTO> dtoList = await HttpService.PostListAsync();
            flightList = new ObservableCollection<Flight>();
            foreach (Flight_DTO dto in dtoList)
            {
                Flight f = new Flight(dto);
                flightList.Add(f);
                Debug.WriteLine(dto);
            }
            ChangedEvent(null, null);
        }

        //Foglalás adatbázis
        public static ObservableCollection<Reservation> GetReservations()
        {
            return reservationList;
        }

        //Járat hozzáadása
        public static void AddFlight(int id, DateTime date, String dep = "London", String dest="New York", String type = "Airbus A370")
        {
            var f = new Flight(id, 7)
            {
                Date = date,
                Departure = dep,
                Destination = dest,
                PlaneType = type,
                Status = "Sceduled",
            };

            //Http kérés kiadása
            HttpService.PostAddFlightAsync(f.ToDTO());
        }

        //Járat törlése
        public static void DeleteFlight(Flight f)
        {
            //Http kérés kiadása
            HttpService.PostDeleteFlightAsync(new DeleteFlight_DTO(f.FlightId));
        }

        //Járat módosítása
        internal static void UpdateFlight(Flight f)
        {
            //Http kérés kiadása
            HttpService.PostUpdateFlightAsync(new UpdateFlight_DTO(f.ToDTO()));
        }

        //Foglalás hozzáadása
        public static void Reserve(int flightId, int seatId)
        {
            flightList[flightId].ReserveSeat(seatId);
            reservationList.Add(new Reservation(flightId,seatId,SignInService.User.Name));

            //Http kérés kiadása
            HttpService.PostReservationAsync(new ReserveSeat_DTO(flightId, seatId));
        }
    }
}
