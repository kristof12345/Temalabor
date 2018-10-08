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

        //A fizetés engedélyezése
        public bool IsPayEnabled { get; set; }

        public String Details { get { return Flight.ToString(); } }

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
