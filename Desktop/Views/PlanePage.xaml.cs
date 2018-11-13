using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DTO;
using Desktop.Models;
using Desktop.UserControls;
using Windows.UI.Xaml.Input;
using Desktop.Dialogs;
using Desktop.ViewModels;
using System.Diagnostics;

namespace Desktop.Views
{
    public sealed partial class PlanePage : Page
    {

        private PlaneViewModel ViewModel
        {
            get { return DataContext as PlaneViewModel; }
        }
        public PlanePage()
        {
            this.InitializeComponent();
        }

        //Fizetés gomb lenyomása
        private async void Pay_Button_Click(object sender, RoutedEventArgs e)
        {
            var reservation = new Reservation(ViewModel.Flight.FlightId);
            foreach (NormalSeatUserControl s in canvas.Children)
            {
                if (s.State == State.Selected)
                {
                    ViewModel.Flight.PlaneType.GetSeatById(s.Seat.SeatId).Reserved = true;
                    reservation.AddSeatId(s.Seat.SeatId); //Összekészítjük a foglalást
                }
            }
            await ViewModel.ReserveAsync(reservation);
            
            this.Frame.Navigate(typeof(MyReservationsPage));
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
            else
            {
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoFlightDialog(this);
            }
        }

        private void AddUserControls()
        {
            for (int i = 0; i < ViewModel.Flight.NumberOfSeats; i++)
            {
                Seat s = ViewModel.Flight.GetSeat(i);
                UserControl newSeat;
                if (s.SeatType == SeatType.Normal)
                {
                    newSeat = new NormalSeatUserControl(s);
                }
                else
                {
                    newSeat = new FirstClassSeatUserControl(s);
                }

                newSeat.Tapped += CalculatePrice; //Eseménykezelő regisztrálása //Left=0, Top=X, Right=Y, Bottom=0
                newSeat.Margin = new Thickness(ViewModel.Flight.GetSeat(i).Coordinates.X, ViewModel.Flight.GetSeat(i).Coordinates.Y, 0, 0);
                canvas.Children.Add(newSeat);
                CalculatePrice(null, null);
            }
        }

        //Kiválasztott székek árának összegzése
        private void CalculatePrice(object sender, TappedRoutedEventArgs e)
        {
            ViewModel.ResetTotalPrice();
            foreach (ISeatUserControl s in canvas.Children)
            {
                if (s.State == State.Selected)
                {
                    if(s.Seat.SeatType==SeatType.Normal)
                        ViewModel.AddToTotalPrice(ViewModel.Flight.NormalPrice);
                    else if (s.Seat.SeatType == SeatType.FirstClass)
                        ViewModel.AddToTotalPrice(ViewModel.Flight.FirstClassPrice);
                }
            }
        }
    }
}
