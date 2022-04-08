using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class LoginPageViewModel
    {
        public LoginPageViewModel()
        {

        }

        //       public Command LoginButtonClicked
        //=> new Command(async () => await Shell.Current.GoToAsync($"//{nameof(FeedPage)}")); //  "//" remove backstack.

        public Command LoginButtonClicked
 => new Command(async () => await loginUser());

        public async Task loginUser()
        {

            await Shell.Current.GoToAsync($"//{nameof(FeedPage)}");  //  "//" remove backstack.
        }


        public Command RegisterLabelTapped
 => new Command(async () => await Shell.Current.GoToAsync($"//{nameof(LoginPage)}/{nameof(RegistrationPage)}"));

    
    }
}