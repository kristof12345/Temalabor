﻿using System;
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
            System.Threading.Thread.Sleep(1000); //Várakozás a szerverre
            await FlightsDataService.Initialize();
            await ReservationsDataService.Initialize();
            await PlaneTypeDataService.Initialize();

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
