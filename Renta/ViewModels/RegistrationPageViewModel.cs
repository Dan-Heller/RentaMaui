using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Renta.Dto_s;
using Renta.Services;

namespace Renta.ViewModels
{
    public class RegistrationPageViewModel : BaseViewModel
    {

        public string password;
        public string email;
        public string FirstName { get; set; }
        public string LastName { get; set;}

        private bool IsFormValid { get; set; } = false;
        private UserService m_userService;
        public RegistrationPageViewModel(UserService userService)
        {
            m_userService = userService;
            password = string.Empty;
            email = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
        }

        public string Password
        {
            set
            {
                password = value;
                OnPropertyChanged();
            }
            get => password;
        }
        public string Email
        {
            set
            {
                email = value;
                OnPropertyChanged();
            }
            get => email;
        }


     




        public Command RegisterCompleted_Clicked
         => new Command(async () => await registerUser());

        private async Task registerUser()
        {
            if(password.Length > 0 && email.Length > 0  && FirstName.Length > 0 && LastName.Length > 0)
            {
                RegisterDto registerDto = new RegisterDto(email, password, FirstName, LastName);
                await m_userService.RegisterUser(registerDto);

                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }
    }
}