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
        public Image profileImage = new Image();
        public ImageSource ProfileImageSource = "addprofileimage.png";

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }


        //public ImageSource ProfileImageSource
        //{
        //    get { return profileImageSource.Source; }
        //    set
        //    {
        //        profileImageSource.Source = value;
        //        OnPropertyChanged();
        //    }
        //}


        public EditProfilePageViewModel()
        {

            profileImage.Source = "addprofileimage.png";
        }

        public Command SaveButtonClicked
       => new Command(async () => await Shell.Current.GoToAsync($"{nameof(ProfilePage)}"));

       // public Command ChangeProfileImage_Clicked
       //=> new Command(async () => await ChangeProfileImage());

       // private async Task ChangeProfileImage()
       // {
       //     var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });

       //     if (result != null)
       //     {
       //         var stream = await result.OpenReadAsync();

       //         //PickedProfileImage.Source = ImageSource.FromStream(() => stream);
       //         //ProfileImageSource = ImageSource.FromStream(() => stream);
       //         ProfileImageSource = "addprofileimage.jpg";
       //         OnPropertyChanged(nameof(ProfileImageSource));
       //     }
       // }

        public Command ChangeProfileImage_Clicked
       => new Command(async () => await Func());

        public async Task  Func()
        {
           
            OnPropertyChanged();
        }
    }
}