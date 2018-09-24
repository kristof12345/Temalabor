using Desktop.Models;
using Desktop.Services;
using DTO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Desktop.Views
{
    public sealed partial class UserPage : Page
    {
        public UserPage()
        {
            this.InitializeComponent();
        }

        //Ha a gombra kattintunk
        private void btLogin_Click(object sender, RoutedEventArgs e)
        {
            //Bejelentkezési kérés elküldése
            if (HttpService.PostLogin(tbName.Text, tbPass.Text))
            {
                User_DTO user = new User_DTO(tbName.Text, tbPass.Text);
                //Ha be van jelölve a checkbox, akkor admin belépés
                if (cbAdmin.IsChecked == true)
                {
                    user.UserType = UserType.Administrator;
                }

                //User elmentése
                SignInService.SignIn(user);

                this.Frame.Navigate(typeof(DataGridPage));
            }
            else tbPass.Text = "Incorrect";
        }
    }
}
