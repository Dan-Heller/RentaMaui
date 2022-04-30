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
        public ImageSource ImageSource1 { get; set; }   
        public ImageSource ImageSource2 { get; set; }
        public ImageSource ImageSource3 { get; set; }
        public ImageSource ImageSource4 { get; set; }

        private string AddPhotoImageSource = "addphoto.jpg";
        public string ItemName { get; set; }
        public string CoinsPerDay { get; set; }
        public string MaxDaysPerRent { get; set; }
        public string ItemDescription { get; set; }
        public string SelectedCategory { get; set; }
        
        private bool ImageAdded = false;

        private FileResult chosenImageFile;
       

        public AddItemPageViewModel()
        {
            Categories = new List<string>();
            Categories.Add("Sports");
            Categories.Add("Clothing");
            Categories.Add("Music");

            

            ImageSource1 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource2 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource3 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource4 = ImageSource.FromFile(AddPhotoImageSource);

        }

        

        public Command AddPhotoFromGallery_Clicked
        => new Command<string>(async (string ImageId) => await AddPhotoFromGallery(ImageId));

        public async Task AddPhotoFromGallery(string ImageId)
        {
            //ImageSource chosenImageSource = getChosenImageSource(ImageId);
            chosenImageFile = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });

            if (chosenImageFile != null)
            {
                UpdateImageSource(ImageId, chosenImageFile);
                ImageAdded = true;
                //return ImageSource1;
            }
         // return ImageSource.FromFile("addphoto.jpg");

        }

        private async void UpdateImageSource(string ImageId,FileResult result)
        {
            var stream = await result.OpenReadAsync();

            switch (ImageId)
            {
                case "1":
                    ImageSource1 = ImageSource.FromStream(() => stream);
                    OnPropertyChanged(nameof(ImageSource1));
                    break;
                case "2":
                    ImageSource2 = ImageSource.FromStream(() => stream);
                    OnPropertyChanged(nameof(ImageSource2));
                    break;
                case "3":
                    ImageSource3 = ImageSource.FromStream(() => stream);
                    OnPropertyChanged(nameof(ImageSource3));
                    break;
                case "4":
                    ImageSource4 = ImageSource.FromStream(() => stream);
                    OnPropertyChanged(nameof(ImageSource4));
                    break;
            }
        }

        public async Task TakeAPhoto(string ImageId)
        {
            var result = await MediaPicker.CapturePhotoAsync();

            if (result != null)
            {
                var stream = await result.OpenReadAsync();

                // resultImage.Source = ImageSource.FromStream(() => stream);
            }
        }


        public Command AddItemClicked
   => new Command(async () => await AddItem());


        private async Task AddItem()
        {
            if(ItemName != null && SelectedCategory != null &&  MaxDaysPerRent != null && int.Parse(MaxDaysPerRent) >= 1 && ImageAdded) //check minimal information inserted.
            {
                await Shell.Current.GoToAsync("..");
            }

            
        }
    }
}
