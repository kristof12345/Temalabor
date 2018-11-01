using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Dialogs;
using Desktop.Models;
using DTO;

namespace Desktop.Services
{
    public static class PlaneTypeDataService
    {
        private static ObservableCollection<PlaneType> typesList = new ObservableCollection<PlaneType>();

        //Foglalás adatbázis
        public static ObservableCollection<PlaneType> PlaneTypeList
        {
            get
            {
                if (typesList == null) { typesList = new ObservableCollection<PlaneType>(); ReloadTypesListAsync(); }
                return typesList;
            }
        }

        //Kapcsolat inicializálása
        public static async Task Initialize()
        {
            try
            {
                typesList = new ObservableCollection<PlaneType>();
                ReloadTypesListAsync();
                //TODO: remove
                for(int i = 0; i < 30; i++)
                {
                    typesList.Add(new PlaneType("abc",1));
                }
            }
            catch (Exception e)
            {
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoServerDialog(null);
            }
        }

        //A foglalások letöltése a szerverről
        private static async void ReloadTypesListAsync()
        {
            List<PlaneType> dtoList = await HttpService.ListPlaneTypesAsync();
            typesList.Clear();
            foreach (PlaneType dto in dtoList)
            {
                typesList.Add(dto);
            }
        }

        //Foglalás hozzáadása
        public static void Reserve(Reservation reserveRequest)
        {
            //Felhasználó beállítása
            reserveRequest.User = SignInService.User.Name;
            //Http kérés kiadása
            HttpService.ReservationAsync(reserveRequest);

            ReloadTypesListAsync();
        }

        //Repülő képek URI-je
        internal static Uri LoadImageUri(long planeTypeID)
        {
            return HttpService.LoadImageUri(planeTypeID);
        }
    }
}
