using MvvmHelpers;
using Newtonsoft.Json;
using Renta.Dto_s;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(UserId), "OwnerId")]
    public class OtherUserProfilePageViewModel : BaseViewModel
    {
        public User _OtherUser { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        private readonly UserService _userService;
        private readonly ReviewsService _reviewService;
        private readonly ChatService _chatService;

        private string _otherUserId;

        public String UserId
        {
            get => _otherUserId;
            set { _otherUserId = Uri.UnescapeDataString(value ?? string.Empty); }
        }

        private List<Item> Items = new List<Item>();

        public ObservableRangeCollection<ItemViewModel> ItemsCollection { get; private set; } =
            new ObservableRangeCollection<ItemViewModel>();

        private List<ItemViewModel> ItemsViewModel;
        private ItemService _itemService;

        public ObservableRangeCollection<UserReview> UserReviewsCollection { get; private set; } =
            new ObservableRangeCollection<UserReview>();

        public OtherUserProfilePageViewModel(UserService userService, ItemService itemService,
            ReviewsService reviewsService, ChatService chatService)
        {
            _userService = userService;
            _itemService = itemService;
            _reviewService = reviewsService;
            _chatService = chatService;
        }

        internal async Task InitializeAsync()
        {
            await FetchUserAsync();
            await FetchUserReviews();
        }

        private async Task FetchUserReviews()
        {
            UserReviewsCollection.ReplaceRange(await _reviewService.GetReviewsOnUser(_OtherUser.Id));
        }

        private async Task FetchUserAsync()
        {
            _OtherUser = await _userService.GetUserById(_otherUserId);
            FullName = _OtherUser.GetFullName();
            Address = _OtherUser.City.Length > 0 ? _OtherUser.Region + ", " + _OtherUser.City : _OtherUser.Region;
            OnPropertyChanged(nameof(_OtherUser));
            OnPropertyChanged(nameof(FullName));
            OnPropertyChanged(nameof(Address));


            Items = await _itemService.GetItemsByOwner(_otherUserId);
            ItemsViewModel = ConvertToViewModels(Items);
            UpdateItemsCollection(ItemsViewModel);
        }

        private List<ItemViewModel> ConvertToViewModels(List<Item> items)
        {
            var viewmodels = new List<ItemViewModel>();
            foreach (var item in items)
            {
                var itemVM = new ItemViewModel(item, _userService);
                viewmodels.Add(itemVM);
            }

            return viewmodels;
        }

        private void UpdateItemsCollection(IEnumerable<ItemViewModel> itemsVm)
        {
            ItemsCollection.ReplaceRange(itemsVm);
        }


        public Command MessageIcon_Tapped
            => new Command(async () => await GoToMessagesPage());

        private async Task GoToMessagesPage()
        {
            var chat = _userService.LoggedInUser.PopulatedChats?.Find(chat =>
                chat.UserA == _otherUserId || chat.UserB == _otherUserId);

            if (chat == null)
            {
                CreateChatDto newChatDto = new CreateChatDto(_userService.LoggedInUser.Id, _OtherUser.Id);
                chat = await _chatService.CreateNewChat(newChatDto);
            }

            var jsonStr = JsonConvert.SerializeObject(chat);
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?chat={jsonStr}");
        }
    }
}