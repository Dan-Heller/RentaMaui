using MvvmHelpers;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    public class MyItemsPageViewModel
    {
        private List<Item> MyItems = new List<Item>();

        public ObservableRangeCollection<ItemViewModel> MyItemsCollection { get; private set; } =
            new ObservableRangeCollection<ItemViewModel>();

        private List<ItemViewModel> MyItemsViewModel;

        private ItemService _itemService;

        private UserService _userService;

        public MyItemsPageViewModel(ItemService itemService, UserService userService)
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
            MyItems = await _itemService.GetLoggedInUserItems();
            MyItemsViewModel = ConvertToViewModels(MyItems);
            MyItemsViewModel = MyItemsViewModel.OrderByDescending(item => item.Item.UploadDate).ToList();
            UpdateMyItemsCollection(MyItemsViewModel);
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
            MyItemsCollection.ReplaceRange(myItemsVM);
        }
    }
}