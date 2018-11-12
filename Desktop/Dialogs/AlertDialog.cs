using Desktop.Services;
using Desktop.Views;
using System;
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
            if(SignInService.IsSignedIn && SignInService.User.UserType == DTO.UserType.Administrator)
                origin.Frame.Navigate(typeof(FlightsPage));
            else
                origin.Frame.Navigate(typeof(MyFlightsPage));
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

        //Dialógusablak (Server not responding.)
        public async void DisplayNoServerDialog(Page origin)
        {
            ContentDialog noUser = new ContentDialog
            {
                Title = "Unable to connect to server.",
                Content = "Plese connect to server or compile the Demo classes.",
                CloseButtonText = "Ok"
            };
            ContentDialogResult result = await noUser.ShowAsync();
        }
    }
}
