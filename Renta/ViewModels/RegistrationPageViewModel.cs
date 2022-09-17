using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using Renta.Dto_s;
using Renta.enums;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    public class RegistrationPageViewModel : BaseViewModel
    {
        public string password;
        public string email;
        public string FirstName { get; set; }
        public string LastName { get; set;}        
        public List<string> Categories { get; set; } = new List<string>();
        public string SelectedCategory1 { get; set; } = string.Empty;
        public string SelectedCategory2 { get; set; } = string.Empty;
        public List<ECategories> SelectedFavoritesCategories { get; set; } = new List<ECategories>();

        public List<string> Regions { get; set; }
        public string SelectedRegion { get; set; }
        public string SelectedCity { get; set; }
        private bool IsFormValid { get; set; } = false;
        private UserService m_userService;
        public RegistrationPageViewModel(UserService userService)
        {
            m_userService = userService;
            password = string.Empty;
            email = string.Empty;
            FirstName = string.Empty;
            LastName = string.Empty;
            SelectedCity = string.Empty;
            SelectedRegion = String.Empty;

            FetchRegionsFromString();
                     
            foreach (var value in Enum.GetNames(typeof(ECategories)))
            {
                Categories.Add(value);
            }
            Categories.RemoveAt(0);
           
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
            if(SelectedCategory1 != string.Empty)
            {
                SelectedFavoritesCategories.Add( Enum.Parse<ECategories>(SelectedCategory1));
            }
            if (SelectedCategory2 != string.Empty)
            {
                SelectedFavoritesCategories.Add(Enum.Parse<ECategories>(SelectedCategory2));
            }

            if (password.Length > 0 && email.Length > 0  && FirstName.Length > 0 && LastName.Length > 0 && SelectedRegion.Length > 0 && SelectedFavoritesCategories.Count == 2)
            {
                RegisterDto registerDto = new RegisterDto(email, password, FirstName, LastName, SelectedCity, SelectedRegion, SelectedFavoritesCategories);
                await m_userService.RegisterUser(registerDto);

                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(" ", "Please fill all the details", "close");
                SelectedFavoritesCategories = new List<ECategories>();
            }           
        }

        private void FetchRegionsFromString()
        {
            string RegionsStr = "Ashkelon \r\nBeer Sheva \r\nBethlehem \r\nGolan \r\nJenin \r\nHasharon \r\nHebron \r\nHadera \r\nHolon \r\nHaifa \r\nTulkarm \r\nJericho \r\nJerusalem \r\nKinneret \r\nNazareth \r\nAcre \r\nAfula \r\nPetah Tikva \r\nSafed \r\nRamallah \r\nRehovot \r\nRamla \r\nRamat Gan \r\nNablus \r\nTel Aviv";
            var stringArr = RegionsStr.Split("\r\n");
            Regions = stringArr.ToList();
        }
    }
}