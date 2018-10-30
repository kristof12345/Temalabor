using DTO;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Desktop.Services
{
    //Singleton osztály
    class SignInService : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SignInService() { }

        private static SignInService instance;

        public static SignInService Instance
        {
            get
            {
                if (instance == null) instance = new SignInService();
                return instance;
            }
        }
        
        public static User User { get; private set; }
        public static async Task<bool> SignInAsync(User u)
        {     
            if (await HttpService.LoginAsync(u))
            {
                User = u;
                Instance.PropertyChanged(Instance, new PropertyChangedEventArgs("Sign In"));
                return true;
            }
            return false;
        }
        public static void SignOut()
        {
            User = null;
            Instance.PropertyChanged(Instance, new PropertyChangedEventArgs("Sign Out"));
        }

        internal static void AddUser(string name, string pass, string type)
        {
            User user = new User(name, pass); //TODO: különböző típusú felhasználók
            HttpService.AddUserAsync(user);
        }
    }
}
