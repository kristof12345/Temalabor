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

        public bool HasReservation
        {
            get { return !(ReservationsDataService.MyReservationList.Count > 0); }
        }

        internal void SelectedAt(int index)
        {
            //TODO
        }
    }
}
