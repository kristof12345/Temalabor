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
    }
}
