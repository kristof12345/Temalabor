using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Desktop.Services;
using DTO;

namespace Desktop.ViewModels
{
    class ReservationViewModel
    {
        public ObservableCollection<Reservation> Source
        {
            get
            {
                return DataService.GetReservations();
            }
        }
    }
}
