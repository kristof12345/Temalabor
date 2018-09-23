using System;
using System.Diagnostics;
using System.IO;
using Desktop.Services;

using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;

namespace Desktop
{
    public sealed partial class App : Application
    {
        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {
            String name="";
            String pass="";

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader("UserFile.txt"))
                {
                    // Read the stream to a string, and write the string to the console.
                    name = sr.ReadLine();
                    pass = sr.ReadLine();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("The file could not be read");
            }

            //Automatikus bejelentkezés
            if (HttpService.PostLogin(name, pass))
            {
                return new ActivationService(this, typeof(ViewModels.DataGridViewModel), new Lazy<UIElement>(CreateShell)); //Kezdő oldal ha sikerül
            }
            else
                return new ActivationService(this, typeof(ViewModels.UserViewModel), new Lazy<UIElement>(CreateShell)); //Kezdő oldal ha nem sikerül
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}
