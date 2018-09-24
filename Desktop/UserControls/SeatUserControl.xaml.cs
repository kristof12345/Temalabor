using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        public string TextBlock { set { textBlock.Text = value; } }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(SeatId);
        }
    }
}
