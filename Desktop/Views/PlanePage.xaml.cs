using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DTO;
using Newtonsoft.Json;
using System.Net.Http;
using Windows.UI.Xaml.Media.Imaging;
using Desktop.ViewModels;
using Desktop.Services;
using Desktop.Models;

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
            HttpService.PostReservation(1, 1);
        }

        //Amikor ide navigálnak, átveszi a paramétereket
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //Beállít egy textboxot
            if (e.Parameter != null)
            {
                Flight f = (Flight)e.Parameter;
                txDetails.Text = "for " + f.ToString();

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
