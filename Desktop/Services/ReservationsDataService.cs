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
        private static ObservableCollection<Reservation> myReservationList = new ObservableCollection<Reservation>();
        private static int sortSwitch=0;

        //Foglalás adatbázis
        public static ObservableCollection<Reservation> ReservationList
        {
            get
            {
                return reservationList;
            }
        }

        internal static ObservableCollection<Reservation> MyReservationList
        {
            get
            {
                ReloadMyReservationListAsync();
                return myReservationList;
            }
        }

        //Inicializálás
        public static async Task Initialize()
        {
            await ReloadReservationListAsync();
        }

        public static void SetSort(int sort)
        {
            sortSwitch = sort;
            ReloadReservationListAsync();
        }

        //A foglalások letöltése a szerverről
        public static async Task ReloadReservationListAsync()
        {
            List<Reservation> dtoList = await HttpService.ListReservationsAsync();

            switch (sortSwitch)
            {
                case 0:
                    dtoList.Sort((x, y) => x.ReservationId.CompareTo(y.ReservationId));
                    break;
                case 1:
                    dtoList.Sort((x, y) => x.UserName.CompareTo(y.UserName));
                    break;
                case 2:
                    dtoList.Sort((x, y) => x.FlightId.CompareTo(y.FlightId));
                    break;
                case 3:
                    dtoList.Sort((x, y) => x.Date.CompareTo(y.Date));
                    break;
                case 4:
                    dtoList.Sort((x, y) => x.Cost.CompareTo(y.Cost));
                    break;
            }
            reservationList.Clear();
            foreach (Reservation dto in dtoList)
            {
                reservationList.Add(dto);
            }
        }

        //A felhasználóhoz tartozó foglalások letöltése a szerverről
        private static async void ReloadMyReservationListAsync()
        {
            List<Reservation> dtoList = await HttpService.ListMyReservationsAsync();
            myReservationList = new ObservableCollection<Reservation>();
            myReservationList.Clear();
            foreach (Reservation dto in dtoList)
            {
                reservationList.Add(dto);
            }
        }

        //Foglalás hozzáadása
        public static async Task ReserveAsync(Reservation reserveRequest)
        {
            //Felhasználó beállítása
            reserveRequest.UserName = SignInService.User.Name;
            reserveRequest.UserID = SignInService.User.UserId;
            //Http kérés kiadása
            await HttpService.AddReservationAsync(reserveRequest);

            ReloadReservationListAsync();
            //Változtak a lefoglalt helyek, így a járatokat is újra kell tölteni
            FlightsDataService.ReloadFlightListAsync();
        }

        internal static async Task DeleteReservationAsync(Reservation selectedItem)
        {
            await HttpService.DeleteReservationAsync(selectedItem);

            ReloadReservationListAsync();
            //Változtak a lefoglalt helyek, így a járatokat is újra kell tölteni
            FlightsDataService.ReloadFlightListAsync();
        }
    }
}
