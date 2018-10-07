using System.Collections.ObjectModel;
using Desktop.Services;
using DTO;

namespace Desktop.ViewModels
{
    public class ReservationViewModel
    {
        public ObservableCollection<Reservation> Source
        {
            get
            {
                return DataService.ReservationList;
            }
        }
    }
}
