using Newtonsoft.Json;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(TransactionString), "transaction")]
    public class TransactionPageViewModel : BaseViewModel
    {
        public Item Item { get; set; }
        private readonly ItemService _itemService;
        private readonly UserService _userService;
        private readonly ReviewsService _reviewService;
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

        public Transaction Transaction { get; set; }

        public string StatusAsString
        {
            get => Transaction.Status.ToString();
        }

        private string _TransactionString;

        public String TransactionString
        {
            get => _TransactionString;
            set { _TransactionString = Uri.UnescapeDataString(value ?? string.Empty); }
        }

        public async Task CreateUserReview()
        {
            UserReview newUserReview = new UserReview();
            newUserReview.ItemId = Transaction.ItemId;
            newUserReview.TransactionId = Transaction.Id;
            newUserReview.ReviewerId = _userService.LoggedInUser.Id;
            newUserReview.Review = UserReview;
            newUserReview.UserRating = float.Parse(SelectedUserRating);
            newUserReview.DateOfReview = DateTime.Now;
            newUserReview.ReviewerName = _userService.LoggedInUser.FirstName + " " + _userService.LoggedInUser.LastName;

            if (_userService.LoggedInUser.Id == Transaction.ItemOwner)
            {
                newUserReview.RevieweeId = Transaction.ItemSeeker;
            }
            else
            {
                newUserReview.RevieweeId = Transaction.ItemOwner;
            }

            await _reviewService.CreateUserReview(newUserReview);
        }

        public async Task CreateItemReview()
        {
            ItemReview newItemReview = new ItemReview();
            newItemReview.ItemId = Transaction.ItemId;
            newItemReview.TransactionId = Transaction.Id;
            newItemReview.SeekerId = _userService.LoggedInUser.Id;
            newItemReview.Review = ItemReview;
            newItemReview.ItemRating = float.Parse(SelectedUserRating);
            newItemReview.OwnerId = Transaction.ItemOwner;
            newItemReview.DateOfReview = DateTime.Now;
            newItemReview.SeekerName = _userService.LoggedInUser.FirstName + " " + _userService.LoggedInUser.LastName;


            await _reviewService.CreateItemReview(newItemReview);
        }

        public async Task deserializeString()
        {
            Transaction = JsonConvert.DeserializeObject<Transaction>(_TransactionString);
            DatesAsString = Transaction.StartDate.Date.ToString("dd/MM/yyyy") + "  -  " +
                            Transaction.EndDate.Date.ToString("dd/MM/yyyy");
            Item = await _itemService.GetItemById(Transaction.ItemId);
            OnPropertyChanged(nameof(Item));
            OnPropertyChanged(nameof(DatesAsString));
        }
    }
}