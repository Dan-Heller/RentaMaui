using Renta.Dto_s;
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
        private string password;
        private string email;
        private UserService m_userService;

        public LoginPageViewModel(UserService userService)
        {
            m_userService = userService;
        }

        public string Password
        {
            set { password = value; }
            get => password;
        }

        public string Email
        {
            set { email = value; }
            get => email;
        }


        public Command LoginButtonClicked
            => new Command(async () => await loginUser());

        public async Task loginUser()
        {
            LoginDto loginDto = new LoginDto(email, password);
            bool LoginSuccess = await m_userService.LoginUser(loginDto);

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