﻿
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

        private User _LoggedUser;

        public ProfilePageViewModel(UserService userService)
        {
            _LoggedUser = userService.LoggedInUser;
            getUserInformation();
            userService.UserUpdatedInvoker += new Action(getUserInformation); //tell me when user info updated .
        }

   
        private void getUserInformation()
        {
            email = _LoggedUser.Email;
            FullName = _LoggedUser.GetFullName();
            ProfileImageUrl = _LoggedUser.ProfilePhotoUrl;
            Address = _LoggedUser.Address;
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