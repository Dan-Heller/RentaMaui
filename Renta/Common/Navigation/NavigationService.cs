using Autofac;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Renta.Common.Navigation
{
    public class NavigationService: INavigationService
    {
        readonly IServiceProvider _services;


        private INavigation Navigation;
        //protected INavigation Navigation
        //{
        //    get
        //    {
        //        INavigation? navigation = Application.Current?.MainPage?.Navigation;
        //        if (navigation is not null)
        //            return navigation;
        //        else
        //        {
        //            //This is not good!
        //            if (Debugger.IsAttached)
        //                Debugger.Break();
        //            throw new Exception();
        //        }
        //    }
        //}
        public NavigationService(IServiceProvider services)
            => _services = services;

        public Task NavigateToProfilePage(INavigation navigation) => NavigateToPage<ProfilePage>(navigation);

        public Task NavigateToMainPage(INavigation navigation) => NavigateToPage<MainPage>(navigation);



        private Task NavigateToPage<T>(INavigation navigation) where T : Page
        {
            Navigation = navigation;
            
            var page = ResolvePage<T>();
            if (page is not null)
                return Navigation.PushAsync(page, true);
            throw new InvalidOperationException($"Unable to resolve type {typeof(T).FullName}");
        }

        private T? ResolvePage<T>() where T : Page
            => _services.GetService<T>();


        public Task NavigateBack()
        {
            if (Navigation.NavigationStack.Count > 1)
                return Navigation.PopAsync();
            throw new InvalidOperationException("No pages to navigate back to!");
        }
    }
}
