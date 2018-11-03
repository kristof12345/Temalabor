using System;

using CommonServiceLocator;

using Desktop.Services;
using Desktop.Views;

using GalaSoft.MvvmLight.Ioc;

namespace Desktop.ViewModels
{
    [Windows.UI.Xaml.Data.Bindable]
    public class ViewModelLocator
    {
        //Nézetek regisztrálása TODO: új nézet regisztrálása
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();

            //FlightView regisztrálása
            Register<FlightViewModel, FlightsPage>();
            //PlaneView regisztrálása
            Register<PlaneViewModel, PlanePage>();
            //UserView regisztrálása
            Register<UserViewModel, UserPage>();
            //ReservationView regisztrálása
            Register<ReservationViewModel, ReservationsPage>();
            //DesignerView regisztrálása
            Register<PlaneTypeDesignerViewModel, PlaneTypeDesignerPage>();
            //PlaneTypeManagerView regisztrálása
            Register<PlaneTypeManagerViewModel, PlaneTypeManagerPage>();
            //MyReservationsView regisztrálása
            Register<MyReservationsViewModel, MyReservationsPage>();
            //MyFlightsView regisztrálása
            Register<MyFlightViewModel, MyFlightsPage>();
        }

        //TODO: az új lapot itt is hozzá kell adni
        public FlightViewModel FlightViewModel => ServiceLocator.Current.GetInstance<FlightViewModel>();

        public UserViewModel UserViewModel => ServiceLocator.Current.GetInstance<UserViewModel>();

        public ReservationViewModel ReservationViewModel => ServiceLocator.Current.GetInstance<ReservationViewModel>();

        public PlaneTypeDesignerViewModel DesignerViewModel => ServiceLocator.Current.GetInstance<PlaneTypeDesignerViewModel>();

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public PlaneViewModel PlaneViewModel => ServiceLocator.Current.GetInstance<PlaneViewModel>();

        public PlaneTypeManagerViewModel PlaneTypeManagerViewModel => ServiceLocator.Current.GetInstance<PlaneTypeManagerViewModel>();

        public MyReservationsViewModel MyReservationsViewModel => ServiceLocator.Current.GetInstance<MyReservationsViewModel>();

        public MyFlightViewModel MyFlightViewModel => ServiceLocator.Current.GetInstance<MyFlightViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
