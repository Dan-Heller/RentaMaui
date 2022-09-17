using Newtonsoft.Json;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(TransactionString), "transaction")]
    public class ActivateTransactionPageViewModel : BaseViewModel
    {
        private readonly FileService _fileService;
        private readonly UserService _userService;
        private readonly TransactionService _transactionService;
        public ImageSource ImageSource1 { get; set; }
        public ImageSource ImageSource2 { get; set; }
        public ImageSource ImageSource3 { get; set; }
        public ImageSource ImageSource4 { get; set; }
        public Transaction Transaction { get; set; }

        private string _transactionString;

        public String TransactionString
        {
            get => _transactionString;
            set { _transactionString = Uri.UnescapeDataString(value ?? string.Empty); }
        }

        public void deserializeString()
        {
            Transaction = JsonConvert.DeserializeObject<Transaction>(_transactionString);
        }

        private string AddPhotoImageSource = "addphoto.jpg";
        private bool ImageAdded = false;
        private readonly int MaxImagesPerItem = 4;
        private FileResult chosenImageFile;
        public List<FileResult> chosenImagesFilesResult = new List<FileResult>();
        private bool[] SlotHasImageArray = new bool[4];

        public ActivateTransactionPageViewModel(FileService fileService, UserService userService,
            TransactionService transactionService)
        {
            _fileService = fileService;
            _userService = userService;
            _transactionService = transactionService;
            ImageSource1 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource2 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource3 = ImageSource.FromFile(AddPhotoImageSource);
            ImageSource4 = ImageSource.FromFile(AddPhotoImageSource);
            _transactionService = transactionService;
        }

        public Command AddPhotoFromGallery_Clicked
            => new Command<string>(async (string ImageId) => await AddPhotoFromGallery(ImageId));

        public async Task AddPhotoFromGallery(string ImageId)
        {
            chosenImageFile =
                await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });
            if (chosenImageFile != null)
            {
                UpdateImageSource(ImageId, chosenImageFile);
            }
        }

        private async void UpdateImageSource(string ImageId, FileResult result)
        {
            var stream = await result.OpenReadAsync();
            SlotHasImageArray[Int32.Parse(ImageId) - 1] = true;
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

        public async Task TakeAPhoto(string imageId)
        {
            chosenImageFile =
                await MediaPicker.CapturePhotoAsync(new MediaPickerOptions { Title = "Please take a photo" });
            if (chosenImageFile != null)
            {
                UpdateImageSource(imageId, chosenImageFile);
            }
        }

        public Command ActivateClicked
            => new Command(async () => await ActivateTransaction());

        private async Task ActivateTransaction()
        {
            var urlsList = new List<string>();

            if (chosenImagesFilesResult.Count > 0) //check minimal information inserted.
            {
                foreach (var fileresult in chosenImagesFilesResult)
                {
                    //upload images to url 
                    var stream = await fileresult.OpenReadAsync();
                    var ImageUrl = await _fileService.UploadImageAsync(stream, fileresult.FileName);
                    urlsList.Add(ImageUrl);
                }

                if (Transaction.ItemOwner == _userService.LoggedInUser.Id)
                {
                    if (Transaction.Status == enums.ETransactionStatus.Approved)
                    {
                        Transaction.OwnerImagesBefore = urlsList;
                        Transaction.OwnerAcceptedActivation = true;
                    }
                    else
                    {
                        Transaction.OwnerImagesAfter = urlsList;
                        Transaction.OwnerAcceptedCompletion = true;
                    }
                }
                else
                {
                    if (Transaction.Status == enums.ETransactionStatus.Approved)
                    {
                        Transaction.SeekerImagesBefore = urlsList;
                        Transaction.SeekerAcceptedActivation = true;
                    }
                    else
                    {
                        Transaction.SeekerImagesAfter = urlsList;
                        Transaction.SeekerAcceptedCompletion = true;
                    }
                }

                await _transactionService.UpdateTransaction(Transaction);
                await _userService.UpdateLoggedInUser();
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(" ", "Pleace add at least one image.", "close");
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