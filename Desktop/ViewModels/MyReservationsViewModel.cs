using Desktop.Services;
using DTO;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;


namespace Desktop.ViewModels
{
    public class MyReservationsViewModel : ViewModelBase
    {
        private bool hasReservation;
        public ObservableCollection<Reservation> Source
        {
            get
            {
                return ReservationsDataService.MyReservationList;
            }
        }

        public bool HasReservation
        {
            get { return !hasReservation; }
            set { hasReservation = value; RaisePropertyChanged("HasReservation"); }
        }

        internal void SelectedAt(int index)
        {
            //TODO
        }
    }
}
