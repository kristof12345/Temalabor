using System;
using System.Collections.ObjectModel;

using Desktop.Models;
using Desktop.Services;

using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        public ObservableCollection<Flight> Source
        {
            get
            {
                // TODO WTS: Replace this with your actual data
                return DataService.GetGridData();
            }
        }
    }
}
