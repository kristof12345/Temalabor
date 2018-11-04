using DTO;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace Desktop.UserControls
{
    public sealed partial class NormalSeatUserControl : UserControl, ISeatUserControl
    {
        public Seat Seat { get; set; }
        public State State { get; private set; }

        public NormalSeatUserControl(Seat s)
        {
            this.InitializeComponent();
            Seat = s;
            if (s.Reserved)
            {
                State = State.Reserved;
                button.Background = new SolidColorBrush(Colors.Red); //Piros
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
                button.Background = new SolidColorBrush(Colors.Blue); //Kék
                State = State.Selected;
            } else if (State == State.Selected) //Ha kijelölt, töröljük a jelölést
            {
                button.Background = new SolidColorBrush(Colors.Green); //Zöld
                State = State.Free;
            }
            //Ha már foglalt, akkor semmit sem csinálunk
        }
    }
}
