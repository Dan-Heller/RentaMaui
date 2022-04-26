using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;


namespace Renta.ViewModels
{
   
    public class FeedPageViewModel 
    {

   

        //public ICommand ProfileCommand { get; private set; }
        //public ICommand CreditsCommand { get; private set; }

        public FeedPageViewModel()
        {
          // _navigationService = navigationService;
           //ProfileCommand = new Command(async () => await GoToUserPage());
          
        }

    

        public Command ProfileButtonTapped
         => new Command(async () => await Shell.Current.GoToAsync($"{nameof(ProfilePage)}"));




     

    }
}