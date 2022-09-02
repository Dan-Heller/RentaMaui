

using Newtonsoft.Json;
using Renta.Dto_s;
using Renta.Models;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCalendar.Maui;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(ItemString), "item")]
    public  class ItemPageViewModel : BaseViewModel
    {
        public bool ItemLiked;
        public ItemViewModel _ItemViewModel { get; set; }

        private UserService _userService { get; set; }
        private TransactionService _transactionService { get; set; }
        private ReviewsService _reviewService { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public DateTime Today { get; set; } = DateTime.Now;

       

        public ObservableRangeCollection<DateTime> datesCollection { get; set; }
        public ObservableRangeCollection<ItemReview> ItemReviewsCollection { get; private set; } = new ObservableRangeCollection<ItemReview>();

        private string _ItemString;
        public String ItemString
        {
            get => _ItemString;
            set
            {
                _ItemString = Uri.UnescapeDataString(value ?? string.Empty);

            }
        }


        public void deserializeString()
        {
            var Item = JsonConvert.DeserializeObject<Item>(_ItemString);
            convertItemToViewModel(Item);
            ItemLiked = _ItemViewModel.ItemLiked;
        }

        public async Task FetchItemReviews()
        {
            ItemReviewsCollection.ReplaceRange(await _reviewService.GetReviewsOnItem(_ItemViewModel.Id));
            //List<ItemReview> itemReviews = await _reviewService.GetReviewsOnItem(_ItemViewModel.Id);
            //ItemReviewsViewModel = await ConvertReviewsToViewModels(itemReviews);
            //ItemReviewsCollection.ReplaceRange(ItemReviewsViewModel);
            ////OnPropertyChanged(nameof(ObservableCollectionCount));
        }

      



        private void convertItemToViewModel(Item item)
        {
            _ItemViewModel = new ItemViewModel(item, _userService);
            OnPropertyChanged(nameof(_ItemViewModel));
        }


        public ItemPageViewModel(UserService userService, TransactionService transactionService, ReviewsService reviewsService)
        {
            
            _userService = userService;
            _transactionService = transactionService;
            _reviewService = reviewsService;
        }

       
        public Command ProfileLink_Tapped
        => new Command(async () => await Shell.Current.GoToAsync($"{nameof(OtherUserProfilePage)}?OwnerId={_ItemViewModel.OwnerId}"));

        public Command SendRequestButton_Clicked
       => new Command(async () => await sendItemRequest());

        public async Task sendItemRequest()
        {
            //check if user has enough coins.
            //string numOfDays = (StartDate - EndDate).ToString();
            var leasingSpan =  datesCollection[datesCollection.Count - 1]  - datesCollection[0];
            int leasingLength = leasingSpan.Days;
            leasingLength++;
            bool enoughCoins = await _userService.GetIfBalanceIsValidForRent((int)_ItemViewModel.PricePerDay, leasingLength);

            if (!enoughCoins)
            {
                await Application.Current.MainPage.DisplayAlert(" ", "You don't have enought RentA coins", "close");
                return;
            }

            
            //check if dates overlap taken dates
            bool IsOverlap = _ItemViewModel.CheckIfRequestedDatesOverlapUnavailableDates(datesCollection[0], datesCollection[datesCollection.Count - 1]);

            if (!IsOverlap)
            {
                var transactionDto = createTransaction();
                await _transactionService.CreateTransaction(transactionDto);
                await Application.Current.MainPage.DisplayAlert(" ", "Request sent, you can watch it on Rental Page.", "close");
            }
            else
            {
                //show messege to ui ...  later convert to imessege 
                await Application.Current.MainPage.DisplayAlert("Failed", "Dates Unavailable", "close");
            }

            
        }

        private CreateTransactionDto createTransaction()
        {
            CreateTransactionDto transaction = new CreateTransactionDto();
            transaction.ItemSeeker = _userService.LoggedInUser.Id;
            transaction.ItemOwner = _ItemViewModel.OwnerId;
            transaction.ItemId = _ItemViewModel.Id;
            transaction.StartDate = datesCollection[0];
            transaction.EndDate = datesCollection[datesCollection.Count - 1];
            transaction.CreatedAt = DateTime.Now;
            return transaction;
        }



    }
}
