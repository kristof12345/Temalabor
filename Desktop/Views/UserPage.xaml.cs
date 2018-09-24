using Desktop.Services;
using DTO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

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

        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            //Kijelentkezünk
            SignInService.SignOut();
            //Újratöltjük az oldalt
            this.Frame.Navigate(typeof(UserPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (SignInService.User != null)
            {
                loginArea.Visibility = Visibility.Collapsed;
                logoutArea.Visibility = Visibility.Visible;
                tbUser2.Text = SignInService.User.Name;
            }
        }
    }
}
