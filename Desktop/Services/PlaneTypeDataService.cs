using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Models;
using DTO;

namespace Desktop.Services
{
    class PlaneTypeDataService
    {
        private static ObservableCollection<PlaneType> typesList;

        public static ObservableCollection<PlaneType> PlaneTypeList
        {
            get
            {
                if (typesList == null)
                {
                    typesList = new ObservableCollection<PlaneType>();
                    ReloadPlaneTypesAsync();
                }
                return typesList;
            }
        }

        private static async void ReloadPlaneTypesAsync()
        {
            if(typesList==null)
                typesList = new ObservableCollection<PlaneType>();
            List<PlaneType> dtoList = await HttpService.ListPlaneTypesAsync();
            typesList.Clear();
            foreach (PlaneType dto in dtoList)
            {
                typesList.Add(dto);
            }
        }

        //Repülő képek URI-je
        internal static Uri LoadImageUri(long planeTypeID)
        {
            return HttpService.LoadImageUri(planeTypeID);
        }
    }
}
