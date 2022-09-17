using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class MenuPageViewModel
    {

        public Command MyItems_Tapped
      => new Command(async () => await Shell.Current.GoToAsync($"{nameof(MyItemsPage)}"));
       
               public Command SavedItems_Tapped
      => new Command(async () => await Shell.Current.GoToAsync($"{nameof(SavedItemsPage)}"));
        
                    public Command Renting_Tapped
      => new Command(async () => await Shell.Current.GoToAsync($"{nameof(RentingPage)}"));


        public Command Rental_Tapped
=> new Command(async () => await Shell.Current.GoToAsync($"{nameof(RentalPage)}"));
    }
}