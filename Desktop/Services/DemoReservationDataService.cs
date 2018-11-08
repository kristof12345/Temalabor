using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using DTO;

namespace Desktop.Services
{
    public static class ReservationsDataService
    {
        private static ObservableCollection<Reservation> reservationList = new ObservableCollection<Reservation>();
        private static ObservableCollection<Reservation> myReservationList;

        //Foglalás adatbázis
        public static ObservableCollection<Reservation> ReservationList
        {
            get
            {
                if (reservationList == null) { reservationList = new ObservableCollection<Reservation>(); }
                return reservationList;
            }
        }

        internal static ObservableCollection<Reservation> MyReservationList
        {
            get
            {
                if (myReservationList == null) { myReservationList = new ObservableCollection<Reservation>(); }
                return myReservationList;
            }
        }

        //A foglalások letöltése a szerverről
        private static async void ReloadReservationListAsync()
        {
            /*
            List<Reservation> dtoList = await HttpService.ListReservationsAsync();
            reservationList.Clear();
            foreach (Reservation dto in dtoList)
            {
                reservationList.Add(dto);
            }
            */
        }

        //Foglalás hozzáadása
        public static void Reserve(Reservation reserveRequest)
        {
            //Felhasználó beállítása
            reserveRequest.User = SignInService.User.Name;
            //Http kérés kiadása
            //HttpService.ReservationAsync(reserveRequest);
            /*
            int flightId = (int) reserveRequest.FlightId;
            foreach (long s in reserveRequest.SeatList)
            {
                FlightsDataService.FlightList[flightId].PlaneType.ReserveSeat((int)s);
            }
            */
            reservationList.Add(reserveRequest);
            //ReloadReservationListAsync();
        }

        internal static async Task ReserveAsync(Reservation reservation)
        {
            reservationList.Add(reservation);
        }

        internal static async Task DeleteReservationAsync(Reservation selectedItem)
        {
            reservationList.Remove(selectedItem);
        }
    }
}
