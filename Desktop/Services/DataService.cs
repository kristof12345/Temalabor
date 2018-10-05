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

        public static event EventHandler FlightListLoadedEvent;

        public static ObservableCollection<Flight> FlightList
        {
            get
            {
                if (flightList == null) { flightList = new ObservableCollection<Flight>(); ReloadListAsync(); }
                return flightList;
            }
        }

        //A járatok letöltése a szerverről
        private static async void ReloadListAsync()
        {
            List<Flight_DTO> dtoList = await HttpService.PostListAsync();
            flightList.Clear();
            foreach (Flight_DTO dto in dtoList)
            {
                Flight f = new Flight(dto);
                flightList.Add(f);
            }
        }

        //Járat hozzáadása
        public static async void AddFlightAsync(long id, DateTime date, String dep = "London", String dest="New York", String type = "Airbus A370")
        {
            var f = new Flight(id)
            {
                Date = date,
                Departure = dep,
                Destination = dest,
                PlaneType = type,
                Status = "Sceduled",
            };

            //Http kérés kiadása
            await HttpService.PostAddFlightAsync(f.ToDTO());

            //Táblázat frissítése
            ReloadListAsync();
        }

        //Járat hozzáadása 2
        public static async void AddFlightAsync(Flight f)
        {
            //Http kérés kiadása
            await HttpService.PostAddFlightAsync(f.ToDTO());

            //Táblázat frissítése
            ReloadListAsync();
        }

        //Járat törlése
        public static async void DeleteFlightAsync(Flight f)
        {
            //Http kérés kiadása
            await HttpService.PostDeleteFlightAsync(f.ToDTO());

            //Táblázat frissítése
            ReloadListAsync();
        }

        //Járat módosítása
        public static async void UpdateFlightAsync(Flight f)
        {
            //Http kérés kiadása
            await HttpService.PostUpdateFlightAsync(f.ToDTO());

            //Táblázat frissítése
            ReloadListAsync();
        }

        private static ObservableCollection<Reservation> reservationList = new ObservableCollection<Reservation>();

        //Foglalás adatbázis
        public static ObservableCollection<Reservation> GetReservations()
        {
            return reservationList;
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
