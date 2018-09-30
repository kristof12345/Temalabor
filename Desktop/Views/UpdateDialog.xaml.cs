using Desktop.Models;
using System;
using Windows.UI.Xaml.Controls;

namespace Desktop.Views
{
    public sealed partial class UpdateDialog : ContentDialog
    {
        //Lekérdezhető propertyk
        public DateTime Date { get { return datePicker.Date.UtcDateTime; } }
        public TimeSpan Time { get { return timePicker.Time; } }
        public String Departure { get { return tbDep.Text; } }
        public String Destination { get { return tbDes.Text; } }
        public String Status { get { return tbSta.Text; } }
        public String PlaneType { get { return (String)comboBox.SelectedItem; } }

        //Konstruktor
        public UpdateDialog(Flight f)
        {
            this.InitializeComponent();
            datePicker.Date = f.Date;
            timePicker.Time = f.Date.TimeOfDay;
            tbDep.Text = f.Departure;
            tbDes.Text = f.Destination;
            tbSta.Text = f.Status;
            comboBox.ItemsSource = PlaneTypes.CreateComboBox();
            comboBox.SelectedItem = f.PlaneType;
        }
    }
}
