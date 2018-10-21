using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DTO;
using Windows.UI.Xaml.Media.Imaging;
using Desktop.Services;
using Desktop.Models;
using Desktop.UserControls;
using System.Collections.Generic;
using System.Diagnostics;
using Windows.UI.Xaml.Input;
using Desktop.Dialogs;
using Desktop.ViewModels;

namespace Desktop.Views
{
    public sealed partial class PlanePage : Page
    {
        //private int totalPrice = 0;

        private PlaneViewModel ViewModel
        {
            get { return DataContext as PlaneViewModel; }
        }
        public PlanePage()
        {
            this.InitializeComponent();
        }

        //Fizetés gomb lenyomása
        private void Pay_Button_Click(object sender, RoutedEventArgs e)
        {
            var reservation = new Reservation(ViewModel.Flight.FlightId);
            foreach (SeatUserControl s in canvas.Children)
            {
                if (s.State == State.Selected)
                {
                    reservation.AddSeatId(s.Seat.SeatId); //Összekészítjük a foglalást
                }
            }
            ViewModel.Reserve(reservation);
            
            this.Frame.Navigate(typeof(PlanePage), ViewModel.Flight); //Az oldal újratöltése
        }

        //Amikor ide navigálnak, átveszi a paramétereket
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                //Az átadott paraméterek értelmezése
                ViewModel.Flight = (Flight)e.Parameter;

                //Kép betöltése
                ViewModel.LoadImage();

                //User controlok felrakása
                AddUserControls();

            //Ha nincs bejelentkezve, vagy nem választott repülőt
            }
            else if(ViewModel.Flight != null)
            {
                //User controlok felrakása
                AddUserControls();
            }
            else if (SignInService.User == null)
            {
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoUserDialog(this);                
            }
            else
            {
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoPlaneDialog(this);
            }
        }

        private void AddUserControls()
        {
            for (int i = 0; i < ViewModel.Flight.NumberOfSeats; i++)
            {
                Seat s = ViewModel.Flight.GetSeat(i);
                SeatUserControl newSeat = new SeatUserControl(s);
                newSeat.Tapped += CalculatePrice; //Eseménykezelő regisztrálása
                                                  //Left=0, Top=X, Right=Y, Bottom=0
                newSeat.Margin = new Thickness(ViewModel.Flight.GetSeat(i).Coordinates.X, ViewModel.Flight.GetSeat(i).Coordinates.Y, 0, 0);
                canvas.Children.Add(newSeat);

                CalculatePrice(null, null);
            }
        }

        //Kiválasztott székek árának összegzése
        private void CalculatePrice(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.ResetTotalPrice();
            foreach (SeatUserControl s in canvas.Children)
            {
                if (s.State == State.Selected)
                {
                    ViewModel.AddToTotalPrice(s.Seat.Price);
                }
            }
        }
    }
}
