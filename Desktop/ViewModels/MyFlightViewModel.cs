using Desktop.Models;
using Desktop.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Desktop.ViewModels
{
    public class MyFlightViewModel: ViewModelBase
    {
        private bool isFlightSelected = false;

        private String dest = "";
        private String dep = "";

        public bool IsFlightSelected
        {
            get { return isFlightSelected; }
            set { isFlightSelected = value; RaisePropertyChanged("IsFlightSelected"); }
        }

        //Adatforrás
        public ObservableCollection<Flight> Source
        {
            get
            {
                //return FlightsDataService.FlightList.Where(x => x.Destination.Contains(dest));
                //return FlightsDataService.FlightList;

                var list = new ObservableCollection<Flight>();
                var data = FlightsDataService.FlightList.Where(x => x.Destination.Contains(dest) && x.Departure.Contains(dep));

                foreach(Flight f in data)
                {
                    list.Add(f);
                }

                return list;
            }
        }

        public String Destination
        {
            get { return dest; }
            set { dest = value; RaisePropertyChanged("Destination"); }
        }

        public String Departure
        {
            get { return dep; }
            set { dep = value; RaisePropertyChanged("Departure"); }
        }
    }
}
