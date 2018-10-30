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
            //DataGridView regisztrálása
            Register<FlightViewModel, FlightsPage>();
            //PlaneView regisztrálása
            Register<PlaneViewModel, PlanePage>();
            //UserView regisztrálása
            Register<UserViewModel, UserPage>();
            //ReservationView regisztrálása
            Register<ReservationViewModel, ReservationsPage>();
            //DesignerView regisztrálása
            Register<DesignerViewModel, DesignerPage>();
            //PlaneTypeManagerView regisztrálása
            Register<PlaneTypeManagerViewModel, PlaneTypeManagerPage>();
            //MyReservationsView regisztrálása
            Register<MyReservationsViewModel, MyReservationsPage>();
        }

        //TODO: az új lapot itt is hozzá kell adni
        public FlightViewModel FlightViewModel => ServiceLocator.Current.GetInstance<FlightViewModel>();

        public ReservationViewModel ReservationViewModel => ServiceLocator.Current.GetInstance<ReservationViewModel>();

        public DesignerViewModel DesignerViewModel => ServiceLocator.Current.GetInstance<DesignerViewModel>();

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public PlaneViewModel PlaneViewModel => ServiceLocator.Current.GetInstance<PlaneViewModel>();

        public PlaneTypeManagerViewModel PlaneTypeManagerViewModel => ServiceLocator.Current.GetInstance<PlaneTypeManagerViewModel>();

        public MyReservationsViewModel MyReservationsViewModel => ServiceLocator.Current.GetInstance<MyReservationsViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
