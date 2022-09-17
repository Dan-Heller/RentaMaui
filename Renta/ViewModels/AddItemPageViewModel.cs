using Renta.enums;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    public class AddItemPageViewModel : BaseViewModel
    {
        private readonly FileService _fileService;
        private readonly UserService _userService;
        private readonly ItemService _itemService;
        public List<string> Categories { get; set; }
        public ImageSource ImageSource1 { get; set; }
        public ImageSource ImageSource2 { get; set; }
        public ImageSource ImageSource3 { get; set; }
        public ImageSource ImageSource4 { get; set; }

        private Item NewItem = new Item();

        private string AddPhotoImageSource = "addphoto.jpg";
        public string ItemName { get; set; }
        public string CoinsPerDay { get; set; }
        public string ItemDescription { get; set; } = string.Empty;
        public string SelectedCategory { get; set; }
        private bool ImageAdded = false;


        private readonly int MaxImagesPerItem = 4;
        private FileResult chosenImageFile;
        public List<FileResult> chosenImagesFilesResult = new List<FileResult>();
        private bool[] SlotHasImageArray = new bool[4];

        public AddItemPageViewModel(FileService fileService, UserService userService, ItemService itemService)
        {
            _fileService = fileService;
            _userService = userService;
            _itemService = itemService;


            Categories = new List<string>();
            Categories.Add(ECategories.Sport.ToString());
            Categories.Add(ECategories.Music.ToString());
            Categories.Add(ECategories.Travel.ToString());
            Categories.Add(ECategories.Clothing.ToString());
            Categories.Add(ECategories.Books.ToString());
            Categories.Add(ECategories.Home.ToString());
            Categories.Add(ECategories.Electronics.ToString());
            Categories.Add(ECategories.Bikes.ToString());
            Categories.Add(ECategories.Tools.ToString());

            ImageSource1 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource2 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource3 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource4 = ImageSource.FromFile(AddPhotoImageSource);
        }


        public Command AddPhotoFromGallery_Clicked
            => new Command<string>(async (string ImageId) => await AddPhotoFromGallery(ImageId));

        public async Task AddPhotoFromGallery(string ImageId)
        {
            chosenImageFile =
                await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });

            if (chosenImageFile != null)
            {
                chosenImageFile.FileName = string.Format(@"{0}{1}", DateTime.Now.Ticks, chosenImageFile.FileName);
                UpdateImageSource(ImageId, chosenImageFile);
            }
        }

        private async void UpdateImageSource(string imageId, FileResult result)
        {
            var stream = await result.OpenReadAsync();
            SlotHasImageArray[Int32.Parse(imageId) - 1] = true;
            switch (imageId)
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

        public async Task TakeAPhoto(string imageId)
        {
            chosenImageFile =
                await MediaPicker.CapturePhotoAsync(new MediaPickerOptions { Title = "Please take a photo" });

            if (chosenImageFile != null)
            {
                UpdateImageSource(imageId, chosenImageFile);
            }
        }


        public Command AddItemClicked
            => new Command(async () => await AddItem());


        private async Task AddItem()
        {
            if (int.Parse(CoinsPerDay) > 100)
            {
                CoinsPerDay = "0";
                OnPropertyChanged(nameof(CoinsPerDay));
                await Application.Current.MainPage.DisplayAlert(" ", "Price should not be more than 2000.", "close");
                return;
            }


            if (ItemName != null && SelectedCategory != null &&
                chosenImagesFilesResult.Count > 0) //check minimal information inserted.
            {
                foreach (var fileresult in chosenImagesFilesResult)
                {
                    //upload images to url 
                    var stream = await fileresult.OpenReadAsync();
                    var imageUrl = await _fileService.UploadImageAsync(stream, fileresult.FileName);
                    NewItem.ImagesUrls.Add(imageUrl);
                }

                NewItem.Name = ItemName;
                NewItem.Category = SelectedCategory;
                NewItem.Description = ItemDescription;
                NewItem.PricePerDay = int.Parse(CoinsPerDay);
                NewItem.OwnerId = _userService.LoggedInUser.Id;
                NewItem.UploadDate = DateTime.Now;
                NewItem.Region = _userService.LoggedInUser.Region;


                await _itemService.UploadNewItem(NewItem);
                await Application.Current.MainPage.DisplayAlert(" ", "Item added successfully!", "close");
                await Shell.Current.GoToAsync("..");
            }
        }


        public void RemoveImage(string imageSlotNumber)
        {
            var numberToInt = (Int32.Parse(imageSlotNumber));
            if (SlotHasImageArray[numberToInt - 1] == true)
            {
                SlotHasImageArray[numberToInt - 1] = false;
                chosenImagesFilesResult.RemoveAt(numberToInt - 1);
                switch (imageSlotNumber)
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