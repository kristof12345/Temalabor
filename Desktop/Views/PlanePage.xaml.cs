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

namespace Desktop.Views
{
    public sealed partial class PlanePage : Page
    {
        private List<Button> seats = new List<Button>();
        private Button btn = new Button();
        public PlanePage()
        {
            this.InitializeComponent();
            txDetails.Text = "no flight selected";
            btn.Width = 500;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Az 1. repülő 1. széke
            HttpService.PostReservationAsync(1, 1);
        }

        //Amikor ide navigálnak, átveszi a paramétereket
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Beállít egy textboxot
            if (e.Parameter != null)
            {
                //Az átadott paraméterek értelmezése
                Flight f = (Flight)e.Parameter;

                txDetails.Text = "for " + f.ToString();

                for (int i = 0; i < f.NumberOfSeats; i++)
                {
                    var bt = new Button();
                    bt.Margin = new Thickness(f.GetSeat(i).Coordinates.X);
                    seats.Add(bt);
                }
     
                //A típus alapján választ képet a repülőről
                switch (f.PlaneType.ToString())
                {
                    case "Boeing777":
                        planeImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/Boeing777white.png"));
                        break;
                    default:
                        planeImg.Source = new BitmapImage(new Uri("ms-appx:///Assets/Antonov124white.png"));
                        break;
                }
            }
        }
    }
}
