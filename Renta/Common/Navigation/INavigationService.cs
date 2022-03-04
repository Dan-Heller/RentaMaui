using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.Common.Navigation
{
    
        public interface INavigationService
        {
            Task NavigateToProfilePage(INavigation nav);

        Task NavigateToMainPage(INavigation nav);
        Task NavigateBack();
        }
 
}
