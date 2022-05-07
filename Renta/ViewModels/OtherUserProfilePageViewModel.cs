using MvvmHelpers;
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
        private UserService _userService;
        
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



        public OtherUserProfilePageViewModel(UserService userService, ItemService itemService)
        {
            _userService = userService;
            _itemService = itemService;
        }

        internal async Task InitializeAsync()
        {
            await FetchUserAsync();
        }

        private async Task FetchUserAsync()
        {
            _OtherUser = await _userService.GetUserById(_OtherUserId);
            FullName = _OtherUser.GetFullName();
            OnPropertyChanged(nameof(_OtherUser));
           OnPropertyChanged(nameof(FullName));
           

            Items = await _itemService.GetItemsByOwner(_OtherUserId);
            ItemsViewModel = ConvertToViewModels(Items);
            UpdateItemsCollection(ItemsViewModel);
        }


        private List<ItemViewModel> ConvertToViewModels(List<Item> Items)
        {
            var viewmodels = new List<ItemViewModel>();
            foreach (var item in Items)
            {
                var itemVM = new ItemViewModel(item);
                viewmodels.Add(itemVM);
            }
            return viewmodels;
        }

        private void UpdateItemsCollection(IEnumerable<ItemViewModel> ItemsVM)
        {
            ItemsCollection.ReplaceRange(ItemsVM);
        }





    }
}
