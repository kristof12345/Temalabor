using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Desktop.Services;
using DTO;
using GalaSoft.MvvmLight;

namespace Desktop.ViewModels
{
    public class ReservationViewModel : ViewModelBase
    {
        private bool isReservationSelected = false;
        private int selectedSort = 0;

        public ObservableCollection<Reservation> Source
        {
            get
            {
                return ReservationsDataService.ReservationList;
            }
        }

        public bool IsReservationSelected
        {
            get { return isReservationSelected; }
            set { isReservationSelected = value; RaisePropertyChanged("IsReservationSelected"); }
        }

        internal async Task DeleteReservationAsync(Reservation deleteRequest)
        {
            await ReservationsDataService.DeleteReservationAsync(deleteRequest);
            IsReservationSelected = false;
        }

        public string[] Sort
        {
            get
            {
                string[] array = { "ReservationID", "UserID", "Cost" };
                return array;
            }
        }

        public int SelectedSort
        {
            get
            {
                return selectedSort;
            }
            set
            {
                selectedSort = value;
                RaisePropertyChanged("SelectedSort");
                ReservationsDataService.SetSort(selectedSort);
            }
        }
    }
}
