using Desktop.Services;
using DTO;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Desktop.ViewModels
{
    public class MyReservationsViewModel
    {
        public ObservableCollection<Reservation> Source
        {
            get
            {
                return ReservationsDataService.MyReservationList;
            }
        }

        internal void SelectedAt(int index)
        {
            //TODO
        }
    }
}
