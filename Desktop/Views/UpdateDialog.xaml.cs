using Desktop.Models;
using System;
using System.Diagnostics;
using Windows.UI.Xaml.Controls;

namespace Desktop.Views
{
    public sealed partial class UpdateDialog : ContentDialog
    {
        private Flight Flight { get; set; }

        //Konstruktor
        public UpdateDialog(Flight f)
        {
            this.InitializeComponent();
            Flight = f;
            datePicker.Date = f.Date;
            timePicker.Time = f.Date.TimeOfDay;
            comboBox.ItemsSource = PlaneTypes.CreateComboBox();
        }

        private void OnApplyClicked(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Flight.Date = datePicker.Date.Date + timePicker.Time;
        }
    }
}
