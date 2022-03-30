using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class CategoriesPageViewModel
    {

        public Command CategoryOptionTappedGoBackToSearchPage
   => new Command(async () => await Shell.Current.GoToAsync(".."));


    }
}
