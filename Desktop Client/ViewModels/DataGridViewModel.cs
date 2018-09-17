using System;
using System.Collections.ObjectModel;

using Desktop_Client.Models;
using Desktop_Client.Services;

using GalaSoft.MvvmLight;

namespace Desktop_Client.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        public ObservableCollection<SampleOrder> Source
        {
            get
            {
                // TODO WTS: Replace this with your actual data
                return SampleDataService.GetGridSampleData();
            }
        }
    }
}
