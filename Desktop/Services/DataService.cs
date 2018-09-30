using System;
using System.Collections.ObjectModel;
using Desktop.Models;
using DTO;

namespace Desktop.Services
{
    public static class DataService
    {
        private static ObservableCollection<Flight> flightList = new ObservableCollection<Flight>
        {
            //Alap járat
            new Flight(0, 9) { Date = new DateTime(2017, 05, 24), Departure = "London", Destination = "New York", PlaneType = "Airbus A380", Status = "Cancelled",},
        };

        private static ObservableCollection<Reservation> reservationList = new ObservableCollection<Reservation>();

        //Teljes repülőjárat adatbázis
        public static ObservableCollection<Flight> GetGridData()
        {
            return flightList;
        }

        //Foglalás adatbázis
        public static ObservableCollection<Reservation> GetReservations()
        {
            return reservationList;
        }

        //Járat hozzáadása
        public static void AddFlight(int id, DateTime date, String dep = "London", String dest="New York", String type = "Airbus A370")
        {
            var temp = new Flight(id, 7)
            {
                Date = date,
                Departure = dep,
                Destination = dest,
                PlaneType = type,
                Status = "Sceduled",
            };

            //Hozzáadás a memóriabeli adatbázishoz
            flightList.Add(temp);

            //Http kérés kiadása
            HttpService.PostAddFlightAsync(temp.ToDTO());
        }

        //Járat törlése
        public static void DeleteFlight(Flight f)
        {
            //Törlés a memóriabeli adatbázisból
            flightList.Remove(f);

            //Http kérés kiadása
            HttpService.PostDeleteFlightAsync(new DeleteFlight_DTO(f.FlightId));
        }

        //Járat módosítása
        internal static void UpdateFlight(Flight f)
        {
            //flightList.Add(f);
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
