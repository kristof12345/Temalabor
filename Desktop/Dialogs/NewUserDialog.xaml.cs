using Desktop.Services;
using DTO;
using System;
using Windows.UI.Xaml.Controls;

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
            SignInService.AddUser(tbName.Text, tbPass.Text, (UserType) comboBox.SelectedItem);
        }

    }
}
