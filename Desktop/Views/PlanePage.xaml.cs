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

namespace Desktop.Views
{
    public sealed partial class PlanePage : Page
    {
        Flight f;
        public PlanePage()
        {
            this.InitializeComponent();
            txDetails.Text = "no flight selected";
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Az 1. repülő 1. széke
            
            foreach (SeatUserControl s in myList.Children)
            {
                if (s.State == State.Selected)
                {
                    f.ReserveSeat((int)s.SeatId);
                    Debug.WriteLine("Lefoglalva: " + s.SeatId);
                    //HttpService.PostReservationAsync(f.FlightId,s.SeatId);
                    this.Frame.Navigate(typeof(PlanePage), f); //Az oldal újratöltése
                }
            }
        }

        //Amikor ide navigálnak, átveszi a paramétereket
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Beállít egy textboxot
            if (e.Parameter != null)
            {
                //Az átadott paraméterek értelmezése
                f = (Flight)e.Parameter;

                txDetails.Text = "for " + f.ToString();

                for (int i = 0; i < f.NumberOfSeats; i++)
                {
                    Seat_DTO s = f.GetSeat(i);
                    SeatUserControl newSeat = new SeatUserControl(s.SeatId, s.Reserved);
                    newSeat.Tapped += CalculatePrice;
                    //Left=0, Top=X, Right=Y, Bottom=0
                    newSeat.Margin = new Thickness(f.GetSeat(i).Coordinates.X, f.GetSeat(i).Coordinates.Y, 0, 0);
                    myList.Children.Add(newSeat);
                }
     
                //A típus alapján választ képet a repülőről
                switch (f.PlaneType.ToString())
                {
                    default:
                        planeImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/Antonov124white.png"));
                        break;
                }
            } else if(e.SourcePageType== typeof(DataGridPage))
            {
                DisplayNoUserDialog();
            } else
            {
                DisplayNoPlaneDialog();
            }
        }

        //Kiválasztott székek árának összegzése
        private void CalculatePrice(object sender, TappedRoutedEventArgs e)
        {
            int totalPrice = 0;
            foreach (SeatUserControl s in myList.Children)
            {
                if (s.State == State.Selected)
                {
                    totalPrice += f.GetSeat((int)s.SeatId).Price;
                }
            }
            txPrice.Text = "Total price: " + totalPrice;
        }

        //Dialógusablak
        private async void DisplayNoPlaneDialog()
        {
            ContentDialog noPlane = new ContentDialog
            {
                Title = "No flight selected",
                Content = "Plese select a flight to continue.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await noPlane.ShowAsync();
            this.Frame.Navigate(typeof(DataGridPage));
        }

        //Dialógusablak
        private async void DisplayNoUserDialog()
        {
            ContentDialog noUser = new ContentDialog
            {
                Title = "You're not signed in.",
                Content = "Plese sign in to continue.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await noUser.ShowAsync();
            this.Frame.Navigate(typeof(UserPage));
        }
    }
}
