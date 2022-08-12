
using Renta.Models;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
       
        private string email;

        private string coins;
        public string FullName { get; set; }
        
        public string Address   { get; set; }

        public string ProfileImageUrl { get; set; }

        public User _LoggedUser { get; set; }

        private UserService _userService;

        public ProfilePageViewModel(UserService userService)
        {
            _userService = userService;
            _LoggedUser = userService.LoggedInUser;
            getUserInformation();
            userService.UserUpdatedInvoker += new Action(getUserInformation); //tell me when user info updated .
        }

        public async Task UpdateUserInfo()
        {
            await _userService.UpdateLoggedInUser();
            _LoggedUser = _userService.LoggedInUser;
            OnPropertyChanged(nameof(_LoggedUser));
        }
   
        private void getUserInformation()
        {
            email = _LoggedUser.Email;
            FullName = _LoggedUser.GetFullName();
            ProfileImageUrl = _LoggedUser.ProfilePhotoUrl;
            Address = _LoggedUser.City.Length > 0 ? _LoggedUser.Region + ", " + _LoggedUser.City : _LoggedUser.Region;
            coins = _LoggedUser.Coins.ToString();

            OnPropertyChanged(nameof(ProfileImageUrl));
            OnPropertyChanged(nameof(FullName));
            OnPropertyChanged(nameof(Address));

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

        public string Coins
        {
            set
            {
                coins = value;
                // OnPropertyChanged();
            }
            get => coins;
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