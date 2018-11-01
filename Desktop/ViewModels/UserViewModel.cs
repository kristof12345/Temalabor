using GalaSoft.MvvmLight;
using System;
using System.Threading.Tasks;
using DTO;
using Desktop.Services;

namespace Desktop.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private String name;
        public String Name
        {
            get { return name; }
            set { name = value; RaisePropertyChanged("Name"); }
        }

        private String pass;
        public String Pass
        {
            get { return pass; }
            set { pass = value; RaisePropertyChanged("Pass"); }
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; RaisePropertyChanged("IsAdmin"); }
        }

        internal async Task LoginAsync()
        {
            User user = new User(Name, Pass);

            //Ha be van jelölve a checkbox, akkor admin belépés
            if (IsAdmin)
            {
                user.UserType = UserType.Administrator;
            }

            //User bejelentkezése
            if (!await SignInService.SignInAsync(user))
            {
                Pass = "Incorrect";
            }
        }

        internal void SignOut()
        {
            SignInService.SignOut();
        }
    }
}
