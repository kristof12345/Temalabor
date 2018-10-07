using Desktop.Services;
using DTO;
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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Desktop.Dialogs
{
    public sealed partial class NewUserDialog : ContentDialog
    {
        public NewUserDialog()
        {
            this.InitializeComponent();
            comboBox.ItemsSource = Enum.GetValues(typeof(UserType));
            comboBox.SelectedIndex = 0;
        }

        private void OnApplyClicked(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            SignInService.AddUser(tbName.Text, tbPass.Text, comboBox.SelectedItem.ToString());
        }

    }
}
