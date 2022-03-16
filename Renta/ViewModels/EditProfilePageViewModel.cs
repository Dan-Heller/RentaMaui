using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class EditProfilePageViewModel
    {
        Image PickedProfileImage { get; set; }
        ImageSource ProfileImageSource { get; set; }

        public EditProfilePageViewModel()
        {
            PickedProfileImage = new Image { Source = "addprofileimage.jpg" };
            ProfileImageSource = PickedProfileImage.Source;
            
        }
        
        public Command SaveButtonClicked
       => new Command(async () => await Shell.Current.GoToAsync($"{nameof(ProfilePage)}"));

        public Command ChangeProfileImage_Clicked
       => new Command(async () =>  await ChangeProfileImage());

        private async Task ChangeProfileImage()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                PickedProfileImage.Source = ImageSource.FromStream(() => stream);
            }

        }

    }
}