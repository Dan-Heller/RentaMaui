using Newtonsoft.Json;
using Renta.Models;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(TransactionString), "transaction")]
    public class ActivateTransactionPageViewModel : BaseViewModel
    {

        private FileService _fileService;
        private UserService _userService;
        private TransactionService _transactionService;      
        public ImageSource ImageSource1 { get; set; }
        public ImageSource ImageSource2 { get; set; }
        public ImageSource ImageSource3 { get; set; }
        public ImageSource ImageSource4 { get; set; }
        public Transaction transaction { get; set; }

        private string _TransactionString;
        public String TransactionString
        {
            get => _TransactionString;
            set
            {
                _TransactionString = Uri.UnescapeDataString(value ?? string.Empty);

            }
        }

        public void deserializeString()
        {
            transaction = JsonConvert.DeserializeObject<Transaction>(_TransactionString);           
        }
       
        private string AddPhotoImageSource = "addphoto.jpg";     
        private bool ImageAdded = false;



        private readonly int MaxImagesPerItem = 4;
        private FileResult chosenImageFile;
        public List<FileResult> chosenImagesFilesResult = new List<FileResult>();
        private bool[] SlotHasImageArray = new bool[4];

        public ActivateTransactionPageViewModel(FileService fileService, UserService userService, TransactionService transactionService)
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
            chosenImageFile = await MediaPicker.PickPhotoAsync(new MediaPickerOptions { Title = "Please pick a photo" });
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

        public async Task TakeAPhoto(string ImageId)
        {
            chosenImageFile = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions { Title = "Please take a photo" });
            if (chosenImageFile != null)
            {
                UpdateImageSource(ImageId, chosenImageFile);
            }
        }

        public Command ActivateClicked
   => new Command(async () => await ActivateTransaction());

        private async Task ActivateTransaction()
        {           
            var UrlsList = new List<string>();

            if (chosenImagesFilesResult.Count > 0) //check minimal information inserted.
            {
                foreach (var fileresult in chosenImagesFilesResult)
                {
                    //upload images to url 
                    var stream = await fileresult.OpenReadAsync();
                    var ImageUrl = await _fileService.UploadImageAsync(stream, fileresult.FileName);
                    UrlsList.Add(ImageUrl);
                }

                if (transaction.ItemOwner == _userService.LoggedInUser.Id)
                {
                    if(transaction.Status == enums.ETransactionStatus.Approved)
                    {
                        transaction.OwnerImagesBefore = UrlsList;
                        transaction.OwnerAcceptedActivation = true;
                    }
                    else
                    {
                        transaction.OwnerImagesAfter = UrlsList;
                        transaction.OwnerAcceptedCompletion = true;
                    }
                   
                }
                else
                {
                    if (transaction.Status == enums.ETransactionStatus.Approved)
                    {
                        transaction.SeekerImagesBefore = UrlsList;
                        transaction.SeekerAcceptedActivation = true;
                    }
                    else
                    {
                        transaction.SeekerImagesAfter = UrlsList;
                        transaction.SeekerAcceptedCompletion = true;
                    }
                }
                             
                await  _transactionService.UpdateTransaction(transaction);
                await _userService.UpdateLoggedInUser();
                await Shell.Current.GoToAsync("..");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(" ", "Pleace add at least one image.", "close");
            }
            
        }
      
        public void RemoveImage(string ImageSlotNumber)
        {
            var NumberToInt = (Int32.Parse(ImageSlotNumber));
            if (SlotHasImageArray[NumberToInt - 1] == true)
            {
                SlotHasImageArray[NumberToInt - 1] = false;
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