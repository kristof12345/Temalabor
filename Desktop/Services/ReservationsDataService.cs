using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DTO;

namespace Desktop.Services
{
    public static class ReservationsDataService
    {
        private static ObservableCollection<Reservation> reservationList;

        //Foglalás adatbázis
        public static ObservableCollection<Reservation> ReservationList
        {
            get
            {
                if (reservationList == null) { reservationList = new ObservableCollection<Reservation>(); ReloadReservationListAsync(); }
                return reservationList;
            }
        }

        //A foglalások letöltése a szerverről
        private static async void ReloadReservationListAsync()
        {
            List<Reservation> dtoList = await HttpService.ListReservationsAsync();
            reservationList.Clear();
            foreach (Reservation dto in dtoList)
            {
                reservationList.Add(dto);
            }
        }

        //Foglalás hozzáadása
        public static async System.Threading.Tasks.Task ReserveAsync(Reservation reserveRequest)
        {
            //Felhasználó beállítása
            reserveRequest.User = SignInService.User.Name;
            //Http kérés kiadása
            await HttpService.ReservationAsync(reserveRequest);

            ReloadReservationListAsync();
            //Változtak a lefoglalt helyek, így a járatokat is újra kell tölteni
            FlightsDataService.ReloadFlightListAsync();
        }

        internal static void DeleteReservation(Reservation selectedItem)
        {
            //TODO:töröljük a kijelölt foglalást
        }
    }
}
