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
    public class EditProfilePageViewModel : INotifyPropertyChanged
    {
        public ImageSource profileImageSource;
        public event PropertyChangedEventHandler PropertyChanged;
        private FileService _fileService;
        public FileResult NewProfileImageFile = null;
       

        public EditProfilePageViewModel(FileService fileService)
        {
            profileImageSource = ImageSource.FromFile("addprofileimage.png");
            _fileService = fileService;
        }


        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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
            var stream = await NewProfileImageFile.OpenReadAsync();
            var str = await _fileService.UploadImageAsync(stream, NewProfileImageFile.FileName);
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
                
            }

            //if (stream != null)
            //{
            //    var str = await _fileService.UploadImageAsync(stream, "ProfilePicture");
            //}
           

        }
    }
}