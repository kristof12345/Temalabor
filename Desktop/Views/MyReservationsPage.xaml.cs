using Desktop.ViewModels;
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
    }
}
