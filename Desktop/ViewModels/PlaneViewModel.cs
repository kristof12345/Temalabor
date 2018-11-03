using Desktop.Models;
using Desktop.Services;
using DTO;
using GalaSoft.MvvmLight;
using System;

namespace Desktop.ViewModels
{
    public class PlaneViewModel : ViewModelBase
    {
        private int totalPrice = 0;
        private bool isPayEnabled = false;

        public Flight Flight;

        private Uri image;

        public Uri Image
        {
            get { return image; }
            private set { image = value; RaisePropertyChanged("Image"); }
        }

        public void LoadImage()
        {
            Image = PlaneTypeDataService.LoadImageUri(Flight.PlaneType.PlaneTypeID);
        }

        //A fizetés engedélyezése
        public bool IsPayEnabled
        {
            get { return isPayEnabled; }
            set { isPayEnabled = value; RaisePropertyChanged("IsPayEnabled"); }
        }

        public String Details
        {
            get
            {
                if (Flight == null) return "";
                return Flight.ToString();
            }
        }

        //A foglalás összege
        public String TotalPrice
        {
            get { return "Total price: " + totalPrice + " $"; }
        }

        public void AddToTotalPrice(int value)
        {
            totalPrice += value;
            RaisePropertyChanged("TotalPrice");
            if (totalPrice > 0)
            {
                IsPayEnabled = true;
            }
            else
            {
                IsPayEnabled = false;
            }
        }

        public void ResetTotalPrice()
        {
            totalPrice = 0;
            RaisePropertyChanged("TotalPrice");
            IsPayEnabled = false;
        }

        internal async System.Threading.Tasks.Task ReserveAsync(Reservation reservation)
        {
            reservation.Cost = totalPrice;
            await ReservationsDataService.ReserveAsync(reservation);
        }
    }
}
