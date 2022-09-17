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
    public class TransactionPageViewModel : BaseViewModel
    {

        public Item Item { get; set; }
        private ItemService _itemService;
        private UserService _userService;
        private ReviewsService _reviewService;
        public string UserReview { get; set; } = string.Empty;
        public string ItemReview { get; set; } = string.Empty;
        public string SelectedUserRating { get; set; } = "5";
        public string SelectedItemRating { get; set; } = "5";

        public TransactionPageViewModel(ItemService itemService, UserService userService, ReviewsService reviewsService)
        {
            _itemService = itemService;
            _userService = userService;
            _reviewService = reviewsService;
        }

        public string DatesAsString { get; set; }

        public Transaction _transaction { get; set; }

        public string StatusAsString { get => _transaction.Status.ToString(); }

        private string _TransactionString;

        public String TransactionString
        {
            get => _TransactionString;
            set
            {
                _TransactionString = Uri.UnescapeDataString(value ?? string.Empty);

            }
        }      
        public async Task CreateUserReview()
        {
           
            UserReview newUserReview = new UserReview();
            newUserReview.ItemId = _transaction.ItemId;
            newUserReview.TransactionId = _transaction.Id;
            newUserReview.ReviewerId = _userService.LoggedInUser.Id;
            newUserReview.Review = UserReview;
            newUserReview.UserRating = float.Parse(SelectedUserRating);
            newUserReview.DateOfReview = DateTime.Now;
            newUserReview.ReviewerName = _userService.LoggedInUser.FirstName + " " + _userService.LoggedInUser.LastName;

            if (_userService.LoggedInUser.Id == _transaction.ItemOwner)
            {
                newUserReview.RevieweeId = _transaction.ItemSeeker;
            }
            else
            {
                newUserReview.RevieweeId = _transaction.ItemOwner;
            }

            await _reviewService.CreateUserReview(newUserReview);
        }
      
        public async Task CreateItemReview()
        {

            ItemReview newItemReview = new ItemReview();
            newItemReview.ItemId = _transaction.ItemId;
            newItemReview.TransactionId = _transaction.Id;
            newItemReview.SeekerId = _userService.LoggedInUser.Id;
            newItemReview.Review = ItemReview;
            newItemReview.ItemRating = float.Parse(SelectedUserRating);
            newItemReview.OwnerId = _transaction.ItemOwner;
            newItemReview.DateOfReview = DateTime.Now;
            newItemReview.SeekerName = _userService.LoggedInUser.FirstName + " " + _userService.LoggedInUser.LastName;


            await _reviewService.CreateItemReview(newItemReview);

        }

        public async Task  deserializeString()
        {
            _transaction = JsonConvert.DeserializeObject<Transaction>(_TransactionString);
            DatesAsString = _transaction.StartDate.Date.ToString("dd/MM/yyyy") + "  -  " + _transaction.EndDate.Date.ToString("dd/MM/yyyy");
            Item = await _itemService.GetItemById(_transaction.ItemId);
            OnPropertyChanged(nameof(Item));
            OnPropertyChanged(nameof(DatesAsString));
        }
    }
}