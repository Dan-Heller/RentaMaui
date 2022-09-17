using Renta.Services;
using Newtonsoft.Json;
using Renta.Models;
using Renta.enums;

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
        public string ItemDescription { get; set; } = string.Empty;
        public string SelectedCategory { get; set; }
        private bool ImageAdded = false;
        private readonly int MaxImagesPerItem = 4;
        private FileResult chosenImageFile;

        public FileResult[] chosenImagesFilesResult = new FileResult[4];
        public bool[] SlotHasImageArray = new bool[4];

        public int[]
            SlotUpdatedImageArray =
                new int[4]; // 0 - no image from start, 1 - image from start but not updated , 2 and above - slot updated

        public ItemViewModel _myItemViewModel { get; set; }
        private string _MyItemString;

        public String MyItemString
        {
            get => _MyItemString;
            set { _MyItemString = Uri.UnescapeDataString(value ?? string.Empty); }
        }

        public void deserializeString()
        {
            var item = JsonConvert.DeserializeObject<Item>(_MyItemString);
            convertItemToViewModelandFetchInfo(item);
        }

        private void convertItemToViewModelandFetchInfo(Item item)
        {
            _myItemViewModel = new ItemViewModel(item, _userService);
            fetchCurrentInfo();
            OnPropertyChanged(nameof(_myItemViewModel));
        }

        private void fetchCurrentInfo()
        {
            int i = 0;
            foreach (var url in _myItemViewModel.ImagesUrls)
            {
                SlotHasImageArray[i] = true;
                SlotUpdatedImageArray[i] = 1;
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

        public Command SaveButtonClicked
            => new Command(async () => await updateItem());

        public EditItemPageViewModel(FileService fileService, UserService userService, ItemService itemService)
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

            ImageSources.Add(ImageSource1);
            ImageSources.Add(ImageSource2);
            ImageSources.Add(ImageSource3);
            ImageSources.Add(ImageSource4);
        }

        public Command AddPhotoFromGallery_Clicked
            => new Command<string>(async (string imageId) => await AddPhotoFromGallery(imageId));

        public async Task AddPhotoFromGallery(string imageId)
        {
            chosenImageFile =
                await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });
            if (chosenImageFile != null)
            {
                UpdateImageSource(imageId, chosenImageFile);
            }
        }

        private async void UpdateImageSource(string imageId, FileResult result)
        {
            var stream = await result.OpenReadAsync();
            SlotHasImageArray[Int32.Parse(imageId) - 1] = true;
            SlotUpdatedImageArray[Int32.Parse(imageId) - 1]++;
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

            chosenImagesFilesResult[Int32.Parse(imageId) - 1] = result;
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


        private async Task updateItem()
        {
            if (ItemName != null && SelectedCategory != null &&
                (SlotHasImageArray.Count((s => s != false)) >
                 0)) //check minimal information inserted. and that image exist
            {
                //image slots that "lost" their image should remove urls from item. (and delete from database in the future..)
                for (int i = 0; i < _myItemViewModel.ImagesUrls.Count; i++)
                {
                    if (SlotHasImageArray[i] == false || SlotUpdatedImageArray[i] > 1)
                    {
                        _myItemViewModel.Item.ImagesUrls.RemoveAt(i);
                    }
                }

                //add new urls.              
                for (int i = 0; i < MaxImagesPerItem; i++)
                {
                    if (chosenImagesFilesResult[i] != null)
                    {
                        //upload images to url 
                        var stream = await chosenImagesFilesResult[i].OpenReadAsync();
                        var imageUrl = await _fileService.UploadImageAsync(stream, chosenImagesFilesResult[i].FileName);
                        if (i == 0)
                        {
                            //add to start of the urls list
                            _myItemViewModel.Item.ImagesUrls.Insert(0, imageUrl);
                        }
                        else
                        {
                            _myItemViewModel.Item.ImagesUrls.Add(imageUrl);
                        }
                    }
                }

                UpdateItemInfoFromEntries();

                await _itemService.UpdateItemInfo(_myItemViewModel.Item);
                await _myItemViewModel.GotoMyUpdatedItemPage();
            }
        }

        private void UpdateItemInfoFromEntries()
        {
            _myItemViewModel.Item.Name = ItemName;
            _myItemViewModel.Item.Category = SelectedCategory;
            _myItemViewModel.Item.Description = ItemDescription;
            _myItemViewModel.Item.PricePerDay = int.Parse(CoinsPerDay);
        }


        public void RemoveImage(string imageSlotNumber)
        {
            var numberToInt = (Int32.Parse(imageSlotNumber));
            if (SlotHasImageArray[numberToInt - 1] == true)
            {
                chosenImagesFilesResult[numberToInt - 1] = null;
                SlotHasImageArray[numberToInt - 1] = false;
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