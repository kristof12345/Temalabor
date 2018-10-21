using DTO;
using System.Threading.Tasks;

namespace Desktop.Services
{
    class SignInService
    {
        public static User User { get; private set; }
        public static async Task<bool> SignInAsync(User u)
        {
            if (await HttpService.LoginAsync(u))
            {
                User = u;
                return true;
            }
            return false;
        }
        public static void SignOut()
        {
            User = null;
        }

        internal static void AddUser(string name, string pass, string type)
        {
            User user = new User(name, pass); //TODO: különböző típusú felhasználók
            HttpService.AddUserAsync(user);
        }
    }
}
