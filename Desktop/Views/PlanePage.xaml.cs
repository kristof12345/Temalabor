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

namespace Desktop.Views
{
    public sealed partial class PlanePage : Page
    {
        public PlanePage()
        {
            this.InitializeComponent();
            txDetails.Text = "no flight selected";

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Az 1. repülő 1. széke
            //HttpService.PostAddFlightAsync();
            Debug.WriteLine("klikk");

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
                    SeatUserControl newSeat = new SeatUserControl();
                    //Left=0, Top=X, Right=Y, Bottom=0
                    newSeat.Margin = new Thickness(0,f.GetSeat(i).Coordinates.X, f.GetSeat(i).Coordinates.Y, 0);
                    //Add(newSeat);
                    myList.Items.Insert(i,newSeat);
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
