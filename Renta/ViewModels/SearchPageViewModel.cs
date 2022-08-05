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
    public class SearchPageViewModel : BaseViewModel
    {
        private List<Item> Items = new List<Item>();
        public ObservableRangeCollection<ItemViewModel> ItemsCollection { get; private set; } = new ObservableRangeCollection<ItemViewModel>();

        public string ObservableCollectionCount { get => ItemsCollection.Count.ToString();}

        private List<ItemViewModel> ItemsViewModel;

        public string SearchText { get; set; } = string.Empty;

        private ItemService _itemService;

        public SearchPageViewModel(ItemService itemService)
        {
            _itemService = itemService;
        }

        internal async Task InitializeAsync()
        {
            await FetchAsync();
        }

        private async Task FetchAsync()
        {
            Items = await _itemService.GetOtherUsersItems();


            //if (podcastsModels == null)
            //{
            //    await Shell.Current.DisplayAlert(
            //        AppResource.Error_Title,
            //        AppResource.Error_Message,
            //        AppResource.Close);

            //    return;
            //}


            ItemsViewModel = ConvertToViewModels(Items);
            UpdateItemsCollection(ItemsViewModel);
            OnPropertyChanged(nameof(ObservableCollectionCount));

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

        public async Task FetchItemsByText()
        {
            var newList = Items.FindAll(x => x.Name.ToLower().Contains(SearchText.Trim().ToLower()));
            ItemsViewModel = ConvertToViewModels(newList);
            UpdateItemsCollection(ItemsViewModel);
            OnPropertyChanged(nameof(ObservableCollectionCount));
        }




        public Command FiltersButton_Tapped
        => new Command(async () => await Shell.Current.GoToAsync($"{nameof(FiltersPage)}"));

        public Command CategoriesButton_Tapped
       => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategoriesPage)}"));

    }
}