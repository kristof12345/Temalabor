using Desktop.Models;
using Desktop.ViewModels;
using Telerik.UI.Xaml.Controls.Grid;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace Desktop.Views
{
    public sealed partial class MyFlightsPage : Page
    {
        private MyFlightViewModel ViewModel
        {
            get { return DataContext as MyFlightViewModel; }
        }

        public MyFlightsPage()
        {
            InitializeComponent();
            //A kijelölt sor változását jelző event
            dataTable.SelectionChanged += selected;
            //A dupla kattintást jelző event
            dataTable.DoubleTapped += doubleTapped;
        }

        //Ha változott a kijelölt sor
        private void selected(object sender, DataGridSelectionChangedEventArgs e)
        {
            if (dataTable.SelectedItem != null)
            {
                ViewModel.IsFlightSelected = true;
            }
            else
            {
                ViewModel.IsFlightSelected = false;
            }
        }

        //Dupla kattintásnál átváltunk a kiválasztott repülő nézetére
        private void doubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (dataTable.SelectedItem != null)
            {
                Flight param = (Flight)dataTable.SelectedItem;
                this.Frame.Navigate(typeof(PlanePage), param);
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.Search();
        }
    }
}
