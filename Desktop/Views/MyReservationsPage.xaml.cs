using Desktop.Services;
using Desktop.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Desktop.Views
{
    public sealed partial class MyReservationsPage : Page
    {
        public MyReservationsPage()
        {
            this.InitializeComponent();
        }

        private MyReservationsViewModel ViewModel
        {
            get { return DataContext as MyReservationsViewModel; }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = listView.SelectedIndex;
            ViewModel.SelectedAt(index);
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyFlightsPage));
        }

        //Amikor ide navigálnak
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            ViewModel.HasReservation = await ReservationsDataService.ReloadMyReservationListAsync();
        }
    }
}
