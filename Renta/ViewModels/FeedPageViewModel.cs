using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Threading.Tasks;
using Android.Net.Wifi.Aware;
using Renta.Services;
using Renta.Models;
using MvvmHelpers;

namespace Renta.ViewModels
{
   
    public class FeedPageViewModel : BaseViewModel
    {
        public ItemService _itemService;
        public UserService _userService;
        private List<Item> Items = new List<Item>();
        public ObservableRangeCollection<ItemViewModel> ItemsNearYouCollection { get; private set; } = new ObservableRangeCollection<ItemViewModel>();
        private List<ItemViewModel> ItemsNearYouViewModel;

        public FeedPageViewModel(UserService userService, ItemService itemService)
        {
            _userService = userService;
            _itemService = itemService;
          
        }

        internal async Task InitializeAsync()
        {
            await FetchNearYouAsync();
        }

        private async Task FetchNearYouAsync()
        {
            Items = await _itemService.GetItemsNearYouByRegion();
            ItemsNearYouViewModel = ConvertToViewModels(Items);
            UpdateItemsNearYouCollection(ItemsNearYouViewModel);
            OnPropertyChanged(nameof(ItemsNearYouCollection));

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

        private void UpdateItemsNearYouCollection(IEnumerable<ItemViewModel> ItemsVM)
        {
            ItemsNearYouCollection.ReplaceRange(ItemsVM);
        }



        public Command ProfileButtonTapped
         => new Command(async () => await Shell.Current.GoToAsync($"{nameof(ProfilePage)}"));




     

    }
}