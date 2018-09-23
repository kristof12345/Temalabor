using Desktop.Services;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Desktop.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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
                //Ha be van jelölve a checkbox, akkor elmentjük az adatokat
                if (cbRemember.IsChecked == true)
                {
                    string[] text = { tbName.Text, tbPass.Text };
                    //SaveUser(text);
                }
                this.Frame.Navigate(typeof(DataGridPage));
            }
            else tbPass.Text = "Incorrect";
        }

        private void SaveUser(string[] text)
        {
            try
            {   // Open the text file using a stream writer.
                using (StreamWriter sw = new StreamWriter("UserFile.txt"))
                {
                    sw.WriteLine(text[1]);
                    sw.WriteLine(text[2]);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("The file could not be opened");
            }
        }
    }
}
