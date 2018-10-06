using Desktop.Services;
using Desktop.UserControls;
using Desktop.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace Desktop.Views
{
    public sealed partial class DesignerPage : Page
    {
        public DesignerPage()
        {
            this.InitializeComponent();
            canvas.PointerPressed += clicked;
        }

        private void clicked(object sender, PointerRoutedEventArgs e)
        {
            Debug.WriteLine("Click");
            var pos = e.GetCurrentPoint(canvas);
            tbCordX.Text = pos.Position.X.ToString();
            tbCordY.Text = pos.Position.Y.ToString();
            SeatUserControl newSeat = new SeatUserControl(1, false);
            //Left=0, Top=X, Right=Y, Bottom=0
            newSeat.Margin = new Thickness(pos.Position.X-10, pos.Position.Y-15, 0, 0);
            canvas.Children.Add(newSeat);
            tbNum.Text = ViewModel.NumberOfSeats;
        }

        private DesignerViewModel ViewModel
        {
            get { return DataContext as DesignerViewModel; }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(SignInService.User == null)
            {
                DisplayNoUserDialog();
            }
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
