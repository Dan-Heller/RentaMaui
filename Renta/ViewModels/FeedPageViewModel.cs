using Renta.Services;
using Renta.Models;
using MvvmHelpers;

namespace Renta.ViewModels
{
    public class FeedPageViewModel : BaseViewModel
    {
        public ItemService _itemService;
        public UserService _userService { get; set; }
        private List<Item> Items = new List<Item>();

        public ObservableRangeCollection<ItemViewModel> ItemsNearYouCollection { get; private set; } =
            new ObservableRangeCollection<ItemViewModel>();

        public ObservableRangeCollection<ItemViewModel> ItemsMightLikeCollection { get; private set; } =
            new ObservableRangeCollection<ItemViewModel>();

        public ObservableRangeCollection<ItemViewModel> NewestItemsCollection { get; private set; } =
            new ObservableRangeCollection<ItemViewModel>();

        private List<ItemViewModel> ItemsNearYouViewModel;
        private List<ItemViewModel> ItemsMightLikeViewModel;
        private List<ItemViewModel> NewestItemsViewModel;

        public FeedPageViewModel(UserService userService, ItemService itemService)
        {
            _userService = userService;
            _itemService = itemService;
        }

        internal async Task InitializeAsync()
        {
            await FetchNearYouAsync();
            await FetchMightLikeItems();
            await FetchNewestItems();
        }

        private async Task FetchNearYouAsync()
        {
            Items = await _itemService.GetItemsNearYouByRegion();
            ItemsNearYouViewModel = ConvertToViewModels(Items);
            UpdateItemsNearYouCollection(ItemsNearYouViewModel);
            OnPropertyChanged(nameof(ItemsNearYouCollection));
        }

        private async Task FetchNewestItems()
        {
            Items = await _itemService.GetNewestItems();
            NewestItemsViewModel = ConvertToViewModels(Items);
            UpdateNewestItemsCollection(NewestItemsViewModel);
            OnPropertyChanged(nameof(NewestItemsCollection));
        }

        private async Task FetchMightLikeItems()
        {
            Items = await _itemService.GetItemsByFavoritesCategories();
            ItemsMightLikeViewModel = ConvertToViewModels(Items);
            UpdateItemsMightLikeCollection(ItemsMightLikeViewModel);
            OnPropertyChanged(nameof(ItemsMightLikeCollection));
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

        private void UpdateItemsNearYouCollection(IEnumerable<ItemViewModel> itemsVm)
        {
            ItemsNearYouCollection.ReplaceRange(itemsVm);
        }

        private void UpdateItemsMightLikeCollection(IEnumerable<ItemViewModel> itemsVm)
        {
            ItemsMightLikeCollection.ReplaceRange(itemsVm);
        }

        private void UpdateNewestItemsCollection(IEnumerable<ItemViewModel> itemsVm)
        {
            NewestItemsCollection.ReplaceRange(itemsVm);
        }


        public Command ProfileButtonTapped
            => new Command(async () => await Shell.Current.GoToAsync($"{nameof(ProfilePage)}"));
    }
}