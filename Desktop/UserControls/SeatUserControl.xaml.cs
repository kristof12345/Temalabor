using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Desktop.UserControls
{
    public sealed partial class SeatUserControl : UserControl
    {
        public long SeatId { get; set; }

        public SeatUserControl()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            button.Background = new SolidColorBrush(Color.FromArgb(255,0,0,255));
        }
    }
}
