using Desktop.Services;
using DTO;
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
                //return ReservationsDataService.ReservationList.Where(x => x.User == SignInService.User.Name);
                return ReservationsDataService.ReservationList;
            }
        }
    }
}
