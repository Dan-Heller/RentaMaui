
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class ProfilePageViewModel
    {
        

        public ProfilePageViewModel()
        {
            
        }

        //public Command LogoutClicked
        // => new Command(async () => await  Shell.Current.GoToAsync($"///{nameof(LoginPage)}"));

        public Command LogoutClicked
        => new Command(async () => await Logout());

        private async Task Logout()
        {
            await Shell.Current.Navigation.PopAsync();
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            
        }


        public Command EditButtonClicked
        => new Command(async () => await Shell.Current.GoToAsync($"{nameof(EditProfilePage)}"));
    }
}