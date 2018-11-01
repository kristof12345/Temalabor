using GalaSoft.MvvmLight;
using System;
using System.Threading.Tasks;
using DTO;
using Desktop.Services;
using System.ComponentModel;
namespace Desktop.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        public UserViewModel()
        {
            SignInService.Instance.PropertyChanged += this.UserChanged;
        }

        private void UserChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged("IsSignedIn");
            RaisePropertyChanged("IsNotSignedIn");
        }

        public bool IsSignedIn
        {
            get
            {
                return SignInService.IsSignedIn;
            }
        }

        public bool IsNotSignedIn
        {
            get
            {
                return !SignInService.IsSignedIn;
            }
        }

        private String name ="Papp Kristóf";
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

        private bool isAdmin=true;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; RaisePropertyChanged("IsAdmin"); }
        }

        //Bejelentkezés
        internal async Task<bool> LoginAsync()
        {
            if(Name==null || Name=="")
            {
                Name = "Nem lehet üres.";
                return false;
            }

            User user = new User(Name, Pass);

            //Ha be van jelölve a checkbox, akkor admin belépés
            if (IsAdmin)
            {
                user.UserType = UserType.Administrator;
            }

            //User bejelentkezése
            if(await SignInService.SignInAsync(user))
            {
                return true;
            }
            return false;
        }

        //Kijelentkezés
        internal void SignOut()
        {
            SignInService.SignOut();
        }
    }
}
