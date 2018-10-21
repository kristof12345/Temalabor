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

namespace Desktop.Views
{
    public sealed partial class PlanePage : Page
    {
        private Flight f;
        private int totalPrice = 0;
        public PlanePage()
        {
            this.InitializeComponent();
            btPay.Visibility = Visibility.Collapsed; //Amíg nincs összeg, nem mutatjuk
        }

        //Fizetés gomb lenyomása
        private void Pay_Button_Click(object sender, RoutedEventArgs e)
        {
            var reservation = new Reservation(f.FlightId);
            foreach (SeatUserControl s in myList.Children)
            {
                if (s.State == State.Selected)
                {
                    reservation.AddSeatId(s.Seat.SeatId); //Összekészítjük a foglalást
                }
            }

            reservation.Cost = totalPrice;
            ReservationsDataService.Reserve(reservation);
            this.Frame.Navigate(typeof(PlanePage), f); //Az oldal újratöltése
        }

        //Amikor ide navigálnak, átveszi a paramétereket
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter != null)
            {
                //Az átadott paraméterek értelmezése
                f = (Flight)e.Parameter;

                txDetails.Text = f.ToString();

                //User controlok felrakása
                for (int i = 0; i < f.NumberOfSeats; i++)
                {
                    Seat s = f.GetSeat(i);
                    SeatUserControl newSeat = new SeatUserControl(s);
                    newSeat.Tapped += CalculatePrice; //Eseménykezelő regisztrálása
                    //Left=0, Top=X, Right=Y, Bottom=0
                    newSeat.Margin = new Thickness(f.GetSeat(i).Coordinates.X, f.GetSeat(i).Coordinates.Y, 0, 0);
                    myList.Children.Add(newSeat);

                    CalculatePrice(null, null);
                }

                //A típus alapján választ képet a repülőről
                switch (f.PlaneType.ToString())
                {
                    default:
                        planeImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/Antonov124white.png"));
                        break;
                }
            //Ha nincs bejelentkezve, vagy nem választott repülőt
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

        //Kiválasztott székek árának összegzése
        private void CalculatePrice(object sender, TappedRoutedEventArgs e)
        {
            totalPrice = 0;
            foreach (SeatUserControl s in myList.Children)
            {
                if (s.State == State.Selected)
                {
                    totalPrice += s.Seat.Price;
                }
            }
            txPrice.Text = "Total price: " + totalPrice + " $";
            btPay.Visibility = Visibility.Visible;
            //Ha van összeg, akkor elérhető a gomb
            if (totalPrice > 0)
            {
                btPay.IsEnabled = true;
            }
            else //Különben nem
            {
                btPay.IsEnabled = true; //TODO: Ez false kellene, hogy legyen
            }
        }
    }
}
