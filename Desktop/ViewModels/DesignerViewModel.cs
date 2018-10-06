using DTO;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desktop.ViewModels
{
    public class DesignerViewModel : ViewModelBase
    {
        private List<Seat> seats = new List<Seat>();

        public String NumberOfSeats
        {
            get{ return seats.Count.ToString(); }
            set { }
        }
        public void addSeat(double x, double y, int seatType=1)
        {
            Seat s = new Seat(seats.Count);
            seats.Add(s);
            RaisePropertyChanged("NumberOfSeats");
        }
    }
}
