using Desktop.Models;
using Desktop.Services;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class MyFlightViewModel: ViewModelBase
    {
        private bool isFlightSelected = false;

        private String dest;

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
                return FlightsDataService.FlightList;
            }
        }

        public String Dest
        {
            get { return dest; }
            set { dest = value; RaisePropertyChanged("Dest"); RaisePropertyChanged("Source"); }
        }

        internal void Search()
        {
            
        }
    }
}
