using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using DTO;
using Windows.UI.Xaml.Media.Imaging;
using Desktop.Services;
using Desktop.Models;
using Desktop.UserControls;

namespace Desktop.Views
{
    public sealed partial class PlanePage : Page
    {
        //Itt érdemes lenne információt átadni, de akkor nem működik
        public PlanePage()
        {
            this.InitializeComponent();
            txDetails.Text = "no flight selected";
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

                }

                //A típus alapján választ képet a repülőről
                switch (f.PlaneType.ToString())
                {
                    case "Boeing777":
                        SeatUserControl uc = new SeatUserControl();
                        uc.Margin = new Windows.UI.Xaml.Thickness(0);
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
