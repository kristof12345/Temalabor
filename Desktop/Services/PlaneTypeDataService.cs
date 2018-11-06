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
                ReloadTypesListAsync();
            }
            catch (Exception e)
            {
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoServerDialog(null);
            }
        }

        //A foglalások letöltése a szerverről
        public static async void ReloadTypesListAsync()
        {
            List<PlaneType> dtoList = await HttpService.ListPlaneTypesAsync();
            typesList.Clear();
            foreach (PlaneType dto in dtoList)
            {
                typesList.Add(dto);
                Debug.WriteLine("Count: " + dtoList.Count);
            }
        }

        internal static async Task DeletePlaneTypeAsync(PlaneType planeType)
        {
            await HttpService.DeletePlaneTypeAsync(planeType);
            ReloadTypesListAsync();
        }

        //Repülő képek URI-je
        internal static Uri LoadImageUri(long planeTypeID)
        {
            return HttpService.LoadImageUri(planeTypeID);
        }

        internal static async Task AddPlaneTypeAsync(PlaneType request)
        {
           await HttpService.AddPlaneTypeAsync(request);
        }
    }
}
