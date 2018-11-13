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

        public static bool IsSignedIn
        {
            get
            {
                return User != null;
            }
        }

        //Privát konstruktor
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
            Session session = await HttpService.LoginAsync(u);
            if (session.Success)
            {
                User = session.User;
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

        internal static void AddUser(string name, string pass, UserType type)
        {
            User user = new User(name, pass, type); //TODO: különböző típusú felhasználók
            HttpService.AddUserAsync(user);
        }
    }
}
