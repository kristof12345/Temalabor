using System;
using Desktop.Dialogs;
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
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            //System.Threading.Thread.Sleep(1000); //Várakozás a szerverre
            try
            {
                await HttpService.Initialize();

                await FlightsDataService.Initialize();
                await ReservationsDataService.Initialize();
                await PlaneTypeDataService.Initialize();
            }
            catch (Exception e)
            {
                AlertDialog dialog = new AlertDialog();
                dialog.DisplayNoServerDialog(null);
            }

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
            return new ActivationService(this, typeof(ViewModels.UserViewModel), new Lazy<UIElement>(CreateShell)); //Kezdő oldal
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}
