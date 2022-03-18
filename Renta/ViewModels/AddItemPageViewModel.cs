using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public  class AddItemPageViewModel : INotifyPropertyChanged
    {
        public List<string> Categories { get; set; }

        public AddItemPageViewModel()
        {
            Categories = new List<string>();
            Categories.Add("Sports");
            Categories.Add("Clothing");
            Categories.Add("Music");
        }

        public string SelectedCategory;

        public string selectedCategory
        {
            get
            {
                return SelectedCategory;
            }
            set
            {
                SelectedCategory = value;
                OnPropertyChanged();
            }
        }

        public Command AddPhotoFromGallery_Clicked
        => new Command(async () => await AddPhotoFromGallery());

        public async Task AddPhotoFromGallery()
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

              

            }
        }

        public Command AddItemClicked
   => new Command(async () => await Shell.Current.GoToAsync(".."));

        public async Task TakeAPhoto()
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

               // resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }


        void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
