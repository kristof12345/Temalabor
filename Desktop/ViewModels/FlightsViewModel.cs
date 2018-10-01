using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Desktop.Models;
using Desktop.Services;

using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
    public class DataGridViewModel : ViewModelBase
    {
        public int PlaneID = 0;

        //Adatforrás
        public ObservableCollection<Flight> Source
        {
            get
            {
                return DataService.FlightList;
            }
        }

        public void AddFlight(String FlightId, DateTimeOffset DatePicked, TimeSpan TimePicked, String Departure, String Destination, String PlaneType)
        {
            int tempId;
            //Ha nem sikerül parsolni az ID-t, akkor az alap generált, növekvő id-t kapja
            if (!int.TryParse(FlightId, out tempId))
            {
                PlaneID++;
                tempId = PlaneID;
            }

            //Idő összerakása a Date pickerből és a Time pickerből
            DateTime tempTime = CombineDateAndTime(DatePicked, TimePicked);

            //Járat hozzáadása
            DataService.AddFlightAsync(tempId, tempTime, Departure, Destination, PlaneType);
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
