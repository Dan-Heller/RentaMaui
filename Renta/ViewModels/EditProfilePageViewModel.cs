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
    public class EditProfilePageViewModel : BaseViewModel
    {
        public ImageSource profileImageSource;
        private FileService _fileService;
        public FileResult NewProfileImageFile = null;
        public UserService _userService;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        private bool ProfileImageChanged = false;
        public List<string> Regions { get; set; }


        private void FetchRegionsFromString()
        {
            string RegionsStr = "Ashkelon \r\nBeer Sheva \r\nBethlehem \r\nGolan \r\nJenin \r\nHasharon \r\nHebron \r\nHadera \r\nHolon \r\nHaifa \r\nTulkarm \r\nJericho \r\nJerusalem \r\nKinneret \r\nNazareth \r\nAcre \r\nAfula \r\nPetah Tikva \r\nSafed \r\nRamallah \r\nRehovot \r\nRamla \r\nRamat Gan \r\nNablus \r\nTel Aviv";
            var stringArr = RegionsStr.Split("\r\n");
            Regions = stringArr.ToList();
        }




        public EditProfilePageViewModel(FileService fileService, UserService userService)
        {
            //profileImageSource = ImageSource.FromFile("addprofileimage.png");
            _fileService = fileService;
            _userService = userService;

            if(_userService.LoggedInUser.ProfilePhotoUrl == String.Empty)
            {
                profileImageSource = ImageSource.FromFile("addprofileimage.png");
            }
            else
            {
                profileImageSource = _userService.LoggedInUser.ProfilePhotoUrl;
            }
            
            FirstName = _userService.LoggedInUser.FirstName;
            LastName = _userService.LoggedInUser.LastName;
            Region = _userService.LoggedInUser.Region;
            City = _userService.LoggedInUser.City;
            FetchRegionsFromString();
        }


        public ImageSource ProfileImageSource
        {
            get { return profileImageSource; }
            set
            {
                profileImageSource = value;
               OnPropertyChanged();
            }
        }


       

        public Command SaveButtonClicked
       => new Command(async () => await updateUser());

        private async Task updateUser()
        {
            //check if image changed
            if (ProfileImageChanged)
            {
                var stream = await NewProfileImageFile.OpenReadAsync();
                var ImageUrl = await _fileService.UploadImageAsync(stream, NewProfileImageFile.FileName);
                _userService.LoggedInUser.ProfilePhotoUrl = ImageUrl;

            }
            ProfileImageChanged = false;

            //update
            _userService.LoggedInUser.FirstName = FirstName;
            _userService.LoggedInUser.LastName = LastName;
            _userService.LoggedInUser.Region = Region;
            _userService.LoggedInUser.City = City;
            await _userService.UpdateUserInfo(_userService.LoggedInUser);

            await Shell.Current.GoToAsync("..");
        }


        public Command ChangeProfileImage_Clicked
       => new Command(async () => await ChangeProfileImage());

        private async Task ChangeProfileImage()
        {
            NewProfileImageFile = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });
            

            if (NewProfileImageFile != null)
            {
                var stream = await NewProfileImageFile.OpenReadAsync();
                profileImageSource = ImageSource.FromStream(() => stream);
                OnPropertyChanged(nameof(ProfileImageSource));
                ProfileImageChanged = true;
            }
            
            //if (stream != null)
            //{
            //    var str = await _fileService.UploadImageAsync(stream, "ProfilePicture");
            //}
        }
    }
}