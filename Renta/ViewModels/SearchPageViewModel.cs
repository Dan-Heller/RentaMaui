using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class SearchPageViewModel
    {
        

         public Command FiltersButton_Tapped
        => new Command(async () => await Shell.Current.GoToAsync($"{nameof(FiltersPage)}"));

    }
}