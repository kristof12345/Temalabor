using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Desktop.Models;
using Desktop.Services;

using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
    public class FlightsViewModel : ViewModelBase
    {
        public int PlaneID = 0;

        //Adatforrás
        public ObservableCollection<Flight> Source
        {
            get
            {
                return DataService.GetGridData();
            }
        }

        //Segédfüggvény a dátum előállításához
        public DateTime CombineDateAndTime(DateTimeOffset date, TimeSpan time)
        {
            DateTime tempTime = date.UtcDateTime;
            tempTime = tempTime.Date + time;
            return tempTime;
        }
    }
}
