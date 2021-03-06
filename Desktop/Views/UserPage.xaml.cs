﻿using Desktop.Dialogs;
using Desktop.Services;
using DTO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System;
using Desktop.ViewModels;

namespace Desktop.Views
{
    public sealed partial class UserPage : Page
    {
        private UserViewModel ViewModel
        {
            get { return DataContext as UserViewModel; }
        }

        public UserPage()
        {
            this.InitializeComponent();
        }

        //Ha a gombra kattintunk
        private async void btLogin_Click(object sender, RoutedEventArgs e)
        {
            if (await ViewModel.LoginAsync())
            {
                //Ez a ViewModelben kellene
                if (SignInService.User.UserType == UserType.Customer)
                {
                    this.Frame.Navigate(typeof(MyReservationsPage));
                }
                else if (SignInService.User.UserType == UserType.Administrator)
                {
                    this.Frame.Navigate(typeof(FlightsPage));
                }
            }
            else
            {
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoUserDialog(this);
            }
        }

        private void btLogout_Click(object sender, RoutedEventArgs e)
        {
            //Kijelentkezünk
            ViewModel.SignOut();
            //Újratöltjük az oldalt
            this.Frame.Navigate(typeof(UserPage));
        }

        private async void btNew_Click(object sender, RoutedEventArgs e)
        {
            NewUserDialog dialog = new NewUserDialog();
            ContentDialogResult result = await dialog.ShowAsync();
        }
    }
}
