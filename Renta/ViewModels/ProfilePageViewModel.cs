using Renta.Common.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class ProfilePageViewModel
    {
        readonly INavigationService _navigationService;
        public INavigation navigation;

        public ProfilePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }
    }
}