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
            btPay.Visibility = Visibility.Collapsed; //Amíg nincs összeg, nem mutatjuk
        }

        //Fizetés gomb lenyomása
        private void Pay_Button_Click(object sender, RoutedEventArgs e)
        {
            //Minden kiválasztott UC-ra összegezzük az árat
            foreach (SeatUserControl s in myList.Children)
            {
                if (s.State == State.Selected)
                {
                    
                    DataService.Reserve((int)f.FlightId, (int)s.SeatId);
                    this.Frame.Navigate(typeof(PlanePage), f); //Az oldal újratöltése
                }
            }
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
                    Seat_DTO s = f.GetSeat(i);
                    SeatUserControl newSeat = new SeatUserControl(s.SeatId, s.Reserved);
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
            txPrice.Text = "Total price: " + totalPrice + " $";
            btPay.Visibility = Visibility.Visible;
            //Ha van összeg, akkor elérhető a gomb
            if (totalPrice > 0)
            {
                btPay.IsEnabled = true;
            } else //Különben nem
            {
                btPay.IsEnabled = false;
            }
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
