using Renta.Common.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class MainPageViewModel
    {
        //INavigation navigation = App.Current.MainPage.Navigation;
        readonly INavigationService _navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
           
        }
    }
}
