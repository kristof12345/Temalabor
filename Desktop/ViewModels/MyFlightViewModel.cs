using Desktop.Models;
using Desktop.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Desktop.ViewModels
{
    public class MyFlightViewModel : ViewModelBase
    {
        private bool isFlightSelected = false;

        public bool IsFlightSelected
        {
            get { return isFlightSelected; }
            set { isFlightSelected = value; RaisePropertyChanged("IsFlightSelected"); }
        }

        private static ObservableCollection<Flight> flightList = new ObservableCollection<Flight>();

        //Adatforrás
        public ObservableCollection<Flight> Source
        {
            get
            {
                Reload();
                return flightList;
            }
        }

        internal void Reload()
        {
            flightList.Clear();
            IEnumerable<Flight> data;
            if (ShowDate)
            {
                data = FlightsDataService.FlightList.Where(x => x.Destination.Contains(Destination) && x.Departure.Contains(Departure) && x.Date.Date == DayDate.Date);
            }
            else if (ShowInterval)
            {
                data = FlightsDataService.FlightList.Where(x => x.Destination.Contains(Destination) && x.Departure.Contains(Departure) && x.Date.Date >= IntervalStart.Date && x.Date.Date <= IntervalEnd.Date);
            }
            else
            {
                data = FlightsDataService.FlightList.Where(x => x.Destination.Contains(Destination) && x.Departure.Contains(Departure));
            }
            foreach (Flight f in data) { flightList.Add(f); }
        }

        public String Destination { get; set; } = "";

        public String Departure { get; set; } = "";

        public DateTimeOffset DayDate { get; set; } = DateTime.Today;

        public DateTimeOffset IntervalStart { get; set; } = DateTime.Today;

        public DateTimeOffset IntervalEnd { get; set; } = DateTime.Today;

        public bool ShowDate { get; set; } = false;

        public bool ShowInterval { get; set; } = false;

        internal void DisplayAll()
        {
            ShowDate = false;
            RaisePropertyChanged("ShowDate");
            ShowInterval = false;
            RaisePropertyChanged("ShowInterval");
        }

        internal void DisplayDay()
        {
            ShowDate = true;
            RaisePropertyChanged("ShowDate");
            ShowInterval = false;
            RaisePropertyChanged("ShowInterval");
        }

        internal void DisplayInterval()
        {
            ShowDate = false;
            RaisePropertyChanged("ShowDate");
            ShowInterval = true;
            RaisePropertyChanged("ShowInterval");
        }
    }
}
