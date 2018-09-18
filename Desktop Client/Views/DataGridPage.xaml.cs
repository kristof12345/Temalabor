using System;
using Desktop_Client.Services;
using Desktop_Client.ViewModels;

using Windows.UI.Xaml.Controls;

namespace Desktop_Client.Views
{
    public sealed partial class DataGridPage : Page
    {
        private DataGridViewModel ViewModel
        {
            get { return DataContext as DataGridViewModel; }
        }

        public DataGridPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataService.AddFlight(99);
        }

        private void Button_Click_1(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            DataService.Reserve(0,1);
        }

    }
}
