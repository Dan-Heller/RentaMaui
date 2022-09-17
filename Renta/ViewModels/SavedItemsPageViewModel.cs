using MvvmHelpers;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    public class SavedItemsPageViewModel
    {
        private List<Item> SavedItems = new List<Item>();

        public ObservableRangeCollection<ItemViewModel> SavedItemsCollection { get; private set; } =
            new ObservableRangeCollection<ItemViewModel>();

        private List<ItemViewModel> SavedItemsViewModel;
        private ItemService _itemService;
        private UserService _userService;

        public SavedItemsPageViewModel(ItemService itemService, UserService userService)
        {
            _userService = userService;
            _itemService = itemService;
        }

        internal async Task InitializeAsync()
        {
            await FetchAsync();
        }

        private async Task FetchAsync()
        {
            SavedItems = await _itemService.GetLoggedInUserLikedItems();

            SavedItemsViewModel = ConvertToViewModels(SavedItems);
            UpdateMyItemsCollection(SavedItemsViewModel);
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

        private void UpdateMyItemsCollection(IEnumerable<ItemViewModel> myItemsVM)
        {
            SavedItemsCollection.ReplaceRange(myItemsVM);
        }
    }
}