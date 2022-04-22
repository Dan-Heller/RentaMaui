
using Renta.Models;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class ProfilePageViewModel
    {

        private string email;

        public ProfilePageViewModel(UserService userService)
        {
            getUserInformation(userService.LoggedInUser);

        }

        private void getUserInformation(User user)
        {
            email = user.Email;
        }


        public string Email
        {
            set
            {
                email = value;
                // OnPropertyChanged();
            }
            get => email;
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