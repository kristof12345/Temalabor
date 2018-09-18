using System;
using System.Collections.ObjectModel;

using Desktop_Client.Models;
using Desktop_Client.Services;

using GalaSoft.MvvmLight;

namespace Desktop_Client.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        public ObservableCollection<Flight> Source
        {
            get
            {
                // TODO: Replace this with actual data
                return DataService.GetGridData();
            }
        }
    }
}
