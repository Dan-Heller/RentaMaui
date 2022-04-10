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
    public class RegistrationPageViewModel : INotifyPropertyChanged
    {

        public string password;
        public string email;
        private bool IsFormValid { get; set; } = false;
        private UserService m_userService;
        public RegistrationPageViewModel(UserService userService)
        {
            m_userService = userService;
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


        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
            
        }




        public Command RegisterCompleted_Clicked
         => new Command(async () => await registerUser());

        private async Task registerUser()
        {
            RegisterDto registerDto = new RegisterDto(email, password);
            await m_userService.RegisterUser(registerDto);

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}