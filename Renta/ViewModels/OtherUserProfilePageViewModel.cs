using MvvmHelpers;
using Newtonsoft.Json;
using Renta.Dto_s;
using Renta.Models;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{

    [QueryProperty(nameof(UserId), "OwnerId")]
    public class OtherUserProfilePageViewModel : BaseViewModel
    {
        public User _OtherUser { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        private UserService _userService;
        private ReviewsService _reviewService;
        private ChatService _chatService;

        private string _OtherUserId;
        public String UserId
        {
            get => _OtherUserId;
            set
            {
                _OtherUserId = Uri.UnescapeDataString(value ?? string.Empty);

            }
        }

        private List<Item> Items = new List<Item>();
        public ObservableRangeCollection<ItemViewModel> ItemsCollection { get; private set; } = new ObservableRangeCollection<ItemViewModel>();
        private List<ItemViewModel> ItemsViewModel;
        private ItemService _itemService;
        public ObservableRangeCollection<UserReview> UserReviewsCollection { get; private set; } = new ObservableRangeCollection<UserReview>();
        public OtherUserProfilePageViewModel(UserService userService, ItemService itemService, ReviewsService reviewsService, ChatService chatService)
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
            _OtherUser = await _userService.GetUserById(_OtherUserId);
            FullName = _OtherUser.GetFullName();
            Address = _OtherUser.City.Length > 0 ? _OtherUser.Region + ", " + _OtherUser.City : _OtherUser.Region;
            OnPropertyChanged(nameof(_OtherUser));
           OnPropertyChanged(nameof(FullName));
            OnPropertyChanged(nameof(Address));


            Items = await _itemService.GetItemsByOwner(_OtherUserId);
            ItemsViewModel = ConvertToViewModels(Items);
            UpdateItemsCollection(ItemsViewModel);
        }

        private List<ItemViewModel> ConvertToViewModels(List<Item> Items)
        {
            var viewmodels = new List<ItemViewModel>();
            foreach (var item in Items)
            {
                var itemVM = new ItemViewModel(item, _userService);
                viewmodels.Add(itemVM);
            }
            return viewmodels;
        }

        private void UpdateItemsCollection(IEnumerable<ItemViewModel> ItemsVM)
        {
            ItemsCollection.ReplaceRange(ItemsVM);
        }


        public Command MessageIcon_Tapped
       => new Command(async () => await GoToMessagesPage());

        private async Task GoToMessagesPage()
        {
            var chat = _userService.LoggedInUser.PopulatedChats?.Find(chat => chat.UserA == _OtherUserId || chat.UserB == _OtherUserId);

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