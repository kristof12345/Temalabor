using Desktop.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace Desktop.Dialogs
{
    public class AlertDialog
    {
        //Dialógusablak (No plane selected.)
        public async void DisplayNoPlaneDialog(Page origin)
        {
            ContentDialog noPlane = new ContentDialog
            {
                Title = "No flight selected",
                Content = "Plese select a flight to continue.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await noPlane.ShowAsync();
            origin.Frame.Navigate(typeof(FlightsPage));
        }

        //Dialógusablak (No user selected.)
        public async void DisplayNoUserDialog(Page origin)
        {
            ContentDialog noUser = new ContentDialog
            {
                Title = "You're not signed in.",
                Content = "Plese sign in to continue.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await noUser.ShowAsync();
            origin.Frame.Navigate(typeof(UserPage));
        }
    }
}
