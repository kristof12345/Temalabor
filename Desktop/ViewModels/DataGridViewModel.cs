using System;
using System.Collections.ObjectModel;

using Desktop.Models;
using Desktop.Services;

using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        public int PlaneID = 0;

        public ObservableCollection<Flight> Source
        {
            get
            {
                return DataService.GetGridData();
            }
        }

        //Combo box feltöltése
        public object CreateComboBox()
        {
            string[] strArray =
                {
                "Airbus A380",
                "Boeing 747",
                "Boeing 777",
                "Antonov 124",
                //További repülő típusok
            };
            return strArray;
        }
    }
}
