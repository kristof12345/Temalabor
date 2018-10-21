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

        public Flight Flight;

        private Uri image;

        public Uri Image
        {
            get { return image; }
            private set { image = value; RaisePropertyChanged("Image"); }
        }

        public void LoadImage()
        {
            Image = FlightsDataService.LoadImageUri(Flight.PlaneType.PlaneTypeID);
        }

        //A fizetés engedélyezése
        public bool IsPayEnabled { get; set; }

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
        }

        public void ResetTotalPrice()
        {
            totalPrice = 0;
        }

        internal void Reserve(Reservation reservation)
        {
            reservation.Cost = totalPrice;
            ReservationsDataService.Reserve(reservation);
        }
    }
}
