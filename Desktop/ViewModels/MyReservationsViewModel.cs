using Desktop.Services;
using DTO;
using System.Collections.ObjectModel;


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
