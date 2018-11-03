using Desktop.Dialogs;
using Desktop.Services;
using Desktop.UserControls;
using Desktop.ViewModels;
using DTO;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Navigation;

namespace Desktop.Views
{
    public sealed partial class PlaneTypeDesignerPage : Page
    {
        private PlaneTypeDesignerViewModel ViewModel
        {
            get { return DataContext as PlaneTypeDesignerViewModel; }
        }

        public PlaneTypeDesignerPage()
        {
            InitializeComponent();
            canvas.PointerPressed += clicked;
        }

        private void clicked(object sender, PointerRoutedEventArgs e)
        {
            //Egér pozíciójának lekérdezése
            var mousePos = e.GetCurrentPoint(canvas).Position;
            var seatPos = new Point(mousePos.X - 10, mousePos.Y - 15); //Hogy az egér a UserControl középpontjában legyen
            SeatUserControl newSeat = new SeatUserControl(new Seat());
            //Left=X, Top=Y, Right=0, Bottom=0
            newSeat.Margin = new Thickness(seatPos.X, seatPos.Y, 0, 0);
            canvas.Children.Add(newSeat);
            ViewModel.AddSeat(seatPos.X, seatPos.Y);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if(SignInService.User == null)
            {
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoUserDialog(this);
            }
            else
            {
                if (e.Parameter != null)
                {
                    //Módosítás
                    ViewModel.SetPlaneType((PlaneType)e.Parameter);
                }
                //A korábbi székeket visszarajzoljuk
                foreach(Seat s in ViewModel.Seats)
                {
                    SeatUserControl newSeat = new SeatUserControl(s);
                    newSeat.Margin = new Thickness(s.Coordinates.X, s.Coordinates.Y, 0, 0);
                    canvas.Children.Add(newSeat);
                }
            }
        }

        private async void btSave_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.SaveAsync();
            //this.Frame.Navigate(typeof(PlaneTypeManagerPage));
        }

        private void btundo_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.RemoveAt(ViewModel.Seats.Count-1); //Töröljük az utolsót a vászonról
            ViewModel.RemoveLastSeat(); //Töröljük a listából is
        }

        private async void btFilePicker_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.PickImageAsync();
        }
    }
}
