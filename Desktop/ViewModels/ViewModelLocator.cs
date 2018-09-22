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
        //Nézetek regisztrálása FONTOS
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register(() => new NavigationServiceEx());
            SimpleIoc.Default.Register<ShellViewModel>();
            //DataGridView regisztrálása
            Register<DataGridViewModel, DataGridPage>();
            //PlaneView regisztrálása
            Register<PlaneViewModel, PlanePage>();
            //UserView regisztrálása
            Register<UserViewModel, UserPage>();

        }

        public DataGridViewModel DataGridViewModel => ServiceLocator.Current.GetInstance<DataGridViewModel>();

        public ShellViewModel ShellViewModel => ServiceLocator.Current.GetInstance<ShellViewModel>();

        public NavigationServiceEx NavigationService => ServiceLocator.Current.GetInstance<NavigationServiceEx>();

        public void Register<VM, V>()
            where VM : class
        {
            SimpleIoc.Default.Register<VM>();

            NavigationService.Configure(typeof(VM).FullName, typeof(V));
        }
    }
}
