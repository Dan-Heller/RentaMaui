using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Renta.ViewModels
{
    public  class AddItemPageViewModel : BaseViewModel
    {
        public List<string> Categories { get; set; }
        public ImageSource imageSource1 { get; set; }
        public ImageSource ImageSource2 { get; set; }
        public ImageSource ImageSource3 { get; set; }
        public ImageSource ImageSource4 { get; set; }

        public string SelectedCategory;

       

        public AddItemPageViewModel()
        {
            Categories = new List<string>();
            Categories.Add("Sports");
            Categories.Add("Clothing");
            Categories.Add("Music");

            imageSource1 = ImageSource.FromFile("addphoto.jpg");
            ImageSource2 = ImageSource.FromFile("addphoto.jpg");
            ImageSource3 = ImageSource.FromFile("addphoto.jpg");
            ImageSource4 = ImageSource.FromFile("addphoto.jpg");

            

        }

        

        public string selectedCategory
        {
            get
            {
                return SelectedCategory;
            }
            set
            {
                SelectedCategory = value;
                OnPropertyChanged(nameof(ImageSource1));
            }
        }

        public Command AddPhotoFromGallery_Clicked
        => new Command(async () => await AddPhotoFromGallery());

        public async Task AddPhotoFromGallery()
        {
           
            //ImageSource chosenImageSource = getChosenImageSource(ImageId);
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });

            


            if (result != null)
            {
                
                var stream = await result.OpenReadAsync();
                ImageSource1 = ImageSource.FromStream(() => stream);
                OnPropertyChanged(nameof(ImageSource1));
                //return ImageSource1;
            }
         // return ImageSource.FromFile("addphoto.jpg");

        }

        public ImageSource ImageSource1
        {
            get { return imageSource1; }
            set
            {
                imageSource1 = value;
                OnPropertyChanged();
            }
        }

        private ImageSource getChosenImageSource(string Id)
        {
            ImageSource wantedImageSource = null;

            switch (Id)
            {
                case "1":
                    wantedImageSource = ImageSource1;
                    break;
                case "2":
                    wantedImageSource = ImageSource2;
                    break;
                case "3":
                    wantedImageSource = ImageSource3;
                    break;
                case "4":
                    wantedImageSource = ImageSource4;
                    break;
            }
            return wantedImageSource;
        }



        public Command AddItemClicked
   => new Command(async () => await Shell.Current.GoToAsync(".."));

        public async Task TakeAPhoto(string ImageId)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

               // resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }
    }
}
