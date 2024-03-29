﻿using Renta.Dto_s;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class LoginPageViewModel
    {
        public string password = "123";
        public string email = "dan";
        private UserService m_userService;

        public LoginPageViewModel(UserService userService)
        {
            m_userService = userService;
        }

        //       public Command LoginButtonClicked
        //=> new Command(async () => await Shell.Current.GoToAsync($"//{nameof(FeedPage)}")); //  "//" remove backstack.

        public string Password
        {
            set
            {
                password = value;
                //OnPropertyChanged();
            }
            get => password;
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


        public Command LoginButtonClicked
            => new Command(async () => await loginUser());

        public async Task loginUser()
        {
            //hide soft keyboard on android 
            // todo add validation before sending
            LoginDto loginDto = new LoginDto(email, password);
            bool LoginSuccess = await m_userService.LoginUser(loginDto);

            //  "//" remove backstack.
            if (LoginSuccess)
            {
                await Shell.Current.GoToAsync($"//{nameof(FeedPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(" ", "User not found, Please try again.", "close");
            }
        }


        public Command RegisterLabelTapped
            => new Command(async () =>
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}/{nameof(RegistrationPage)}"));
    }
}