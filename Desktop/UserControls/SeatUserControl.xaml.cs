using DTO;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Desktop.UserControls
{
    public enum State
    {
        Reserved,
        Free,
        Selected
    }

    public sealed partial class SeatUserControl : UserControl
    {
        public Seat Seat { get; set; }
        public State State { get; private set; }

        public SeatUserControl(Seat s)
        {
            this.InitializeComponent();
            Seat = s;
            if (s.Reserved)
            {
                State = State.Reserved;
                button.Background = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)); //Piros
            }
            else
            {
                State = State.Free;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (State == State.Free) //Ha szabad, kijelöljük
            {
                button.Background = new SolidColorBrush(Color.FromArgb(255, 0, 0, 255)); //Kék
                State = State.Selected;
            } else if (State == State.Selected) //Ha kijelölt, töröljük a jelölést
            {
                button.Background = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0)); //Zöld
                State = State.Free;
            }
            //Ha már foglalt, akkor semmit sem csinálunk
        }
    }
}
