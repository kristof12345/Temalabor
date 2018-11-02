using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Dialogs;
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
            //List<PlaneType> dtoList = await HttpService.ListPlaneTypesAsync();
            typesList.Clear();
            typesList.Add(new PlaneType("Airbus A380", 1));
            typesList.Add(new PlaneType("Boeing 777", 2));
        }

        //Repülő képek URI-je
        internal static Uri LoadImageUri(long planeTypeID)
        {
            return new Uri("https://image.ibb.co/gOHBVf/Antonov124white.png");
        }
    }
}
