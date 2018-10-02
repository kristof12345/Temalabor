using Desktop.Services;
using Desktop.ViewModels;
using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Desktop.Views
{
    public sealed partial class ReservationsPage : Page
    {
        public ReservationsPage()
        {
            this.InitializeComponent();
        }

        private ReservationViewModel ViewModel
        {
            get { return DataContext as ReservationViewModel; }
        }

        //Amikor erre a lapra érkezünk
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            //User ellenőrzése
            if (SignInService.User == null)
            {
                DisplayNoUserDialog();
            }
            else
            {
                //TODO: kiírjuk a felhasználó foglalásait
                //textBlock.Text = SignInService.User.Name;
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
