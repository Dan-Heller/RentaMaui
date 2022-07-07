using Renta.Services;
using Newtonsoft.Json;
using Renta.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(MyItemString), "item")]
    public class EditItemPageViewModel : BaseViewModel
    {

        private FileService _fileService;
        private UserService _userService;
        private ItemService _itemService;
        public List<string> Categories { get; set; }
        public ImageSource ImageSource1 { get; set; }
        public ImageSource ImageSource2 { get; set; }
        public ImageSource ImageSource3 { get; set; }
        public ImageSource ImageSource4 { get; set; }

        public List<ImageSource> ImageSources = new List<ImageSource>();
        

        private string AddPhotoImageSource = "addphoto.jpg";
        public string ItemName { get; set; }
        public string CoinsPerDay { get; set; }
        //public string MaxDaysPerRent { get; set; }
        public string ItemDescription { get; set; } = string.Empty;
        public string SelectedCategory { get; set; }

        private bool ImageAdded = false;


        private readonly int MaxImagesPerItem = 4;
        private FileResult chosenImageFile;
        private List<FileResult> chosenImagesFilesResult = new List<FileResult>();


        public ItemViewModel _myItemViewModel { get; set; }
        private string _MyItemString;
        public String MyItemString
        {
            get => _MyItemString;
            set
            {
                _MyItemString = Uri.UnescapeDataString(value ?? string.Empty);

            }
        }

        public void deserializeString()
        {
            var Item = JsonConvert.DeserializeObject<Item>(_MyItemString);
            convertItemToViewModel(Item);
        }

        private void convertItemToViewModel(Item item)
        {
            _myItemViewModel = new ItemViewModel(item);
            fetchCurrentInfo();
            OnPropertyChanged(nameof(_myItemViewModel));

         
        }

        private void fetchCurrentInfo()
        {
            int i = 0;
            foreach (var url in _myItemViewModel.ImagesUrls)
            {
                switch (i)
                {
                    case 0:
                        ImageSource1 = url;
                        OnPropertyChanged(nameof(ImageSource1));
                        break;
                    case 1:
                        ImageSource2 = url;
                        OnPropertyChanged(nameof(ImageSource2));
                        break;
                    case 2:
                        ImageSource3 = url;
                        OnPropertyChanged(nameof(ImageSource3));
                        break;
                    case 3:
                        ImageSource4 = url;
                        OnPropertyChanged(nameof(ImageSource4));
                        break;
                }
                i++;

            }

            ItemName = _myItemViewModel.Name;
            CoinsPerDay = _myItemViewModel.PricePerDay.ToString();
            SelectedCategory = _myItemViewModel.Category;
            ItemDescription = _myItemViewModel.Description;
            OnPropertyChanged(nameof(ItemName));
            OnPropertyChanged(nameof(CoinsPerDay));
            OnPropertyChanged(nameof(SelectedCategory));
            OnPropertyChanged(nameof(ItemDescription));
        }

        // public Command SaveButtonClicked
        //=> new Command(async () => await updateUser());

        public EditItemPageViewModel(FileService fileService, UserService userService, ItemService itemService)
        {
            _fileService = fileService;
            _userService = userService;
            _itemService = itemService;


            Categories = new List<string>();
            Categories.Add("Sports");
            Categories.Add("Clothing");
            Categories.Add("Music");



            // ImageSource1 = _myItemViewModel.ImagesUrls[0];
            ImageSource1 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource2 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource3 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource4 = ImageSource.FromFile(AddPhotoImageSource);

            ImageSources.Add(ImageSource1);
            ImageSources.Add(ImageSource2);
            ImageSources.Add(ImageSource3);
            ImageSources.Add(ImageSource4);

        }



        public Command AddPhotoFromGallery_Clicked
        => new Command<string>(async (string ImageId) => await AddPhotoFromGallery(ImageId));

        public async Task AddPhotoFromGallery(string ImageId)
        {
            chosenImageFile = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });
            if (chosenImageFile != null)
            {
                UpdateImageSource(ImageId, chosenImageFile);


            }
        }




        private async void UpdateImageSource(string ImageId, FileResult result)
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
            chosenImagesFilesResult.Add(result);
        }

        public async Task TakeAPhoto(string ImageId)
        {
            chosenImageFile = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions { Title = "Please take a photo" });


            if (chosenImageFile != null)
            {
                UpdateImageSource(ImageId, chosenImageFile);


            }
        }


   //     public Command AddItemClicked
   //=> new Command(async () => await AddItem());


        //private async Task AddItem()
        //{

        //    // MaxDaysPerRent != null && int.Parse(MaxDaysPerRent) >= 1 &&

        //    if (ItemName != null && SelectedCategory != null && chosenImagesFilesResult.Count > 0) //check minimal information inserted.
        //    {
        //        foreach (var fileresult in chosenImagesFilesResult)
        //        {
        //            //upload images to url 
        //            var stream = await fileresult.OpenReadAsync();
        //            var ImageUrl = await _fileService.UploadImageAsync(stream, fileresult.FileName);
        //            NewItem.ImagesUrls.Add(ImageUrl);
        //        }


        //        NewItem.Name = ItemName;
        //        NewItem.Category = SelectedCategory;
        //        NewItem.Description = ItemDescription;
        //        NewItem.PricePerDay = int.Parse(CoinsPerDay);
        //        NewItem.OwnerId = _userService.LoggedInUser.Id;


        //        await _itemService.UploadNewItem(NewItem);
        //        await Shell.Current.GoToAsync("..");
        //    }
        //}


        public void RemoveImage(string ImageSlotNumber)
        {
            var NumberToInt = (Int32.Parse(ImageSlotNumber));
            if (chosenImagesFilesResult.Count >= NumberToInt)
            {
                chosenImagesFilesResult.RemoveAt(NumberToInt - 1);
                switch (ImageSlotNumber)
                {
                    case "1":
                        ImageSource1 = ImageSource.FromFile(AddPhotoImageSource);
                        OnPropertyChanged(nameof(ImageSource1));
                        break;
                    case "2":
                        ImageSource2 = ImageSource.FromFile(AddPhotoImageSource);
                        OnPropertyChanged(nameof(ImageSource2));
                        break;
                    case "3":
                        ImageSource3 = ImageSource.FromFile(AddPhotoImageSource);
                        OnPropertyChanged(nameof(ImageSource3));
                        break;
                    case "4":
                        ImageSource4 = ImageSource.FromFile(AddPhotoImageSource);
                        OnPropertyChanged(nameof(ImageSource4));
                        break;
                }
            }
        }






    }
}