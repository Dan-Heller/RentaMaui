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
        public string Address { get; set; }
        private bool ProfileImageChanged = false;
       






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
            Address = _userService.LoggedInUser.Address;

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
            _userService.LoggedInUser.Address = Address;
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