using MvvmHelpers;
using Renta.enums;
using Renta.Models;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    //[QueryProperty(nameof(CategoryString), "categoryStr")]
    public class SearchPageViewModel : BaseViewModel
    {
        private List<Item> Items = new List<Item>();
        public ObservableRangeCollection<ItemViewModel> ItemsCollection { get; private set; } = new ObservableRangeCollection<ItemViewModel>();

        public string ObservableCollectionCount { get => ItemsCollection.Count.ToString();}

        private List<ItemViewModel> ItemsViewModel;

        private List<ItemViewModel> ItemsViewModelFiltered;

        public string SearchText { get; set; } = string.Empty;

        private SearchPageStateService _searchPageStateService;

        private ItemService _itemService;

        private UserService _userService;

        private string CategoryString;
        //public String CategoryString
        //{
        //    get => _CategoryString;
        //    set
        //    {
        //        _CategoryString = Uri.UnescapeDataString(value ?? string.Empty);

        //    }
        //}




        public SearchPageViewModel(ItemService itemService, SearchPageStateService searchPageStateService, UserService userService)
        {
            _itemService = itemService;
            _searchPageStateService = searchPageStateService;
            _userService = userService;
        }

        internal async Task InitializeAsync()
        {
            await FetchAsync();
        }

        private async Task FetchAsync()
        {
            Items = await _itemService.GetOtherUsersItems();
            CategoryString = _searchPageStateService._CategoryString;


            //if (podcastsModels == null)
            //{
            //    await Shell.Current.DisplayAlert(
            //        AppResource.Error_Title,
            //        AppResource.Error_Message,
            //        AppResource.Close);

            //    return;
            //}


            ItemsViewModelFiltered = ItemsViewModel = ConvertToViewModels(Items);
           

            FilterByCategory();
            FilterByPriceRange();
            FilterByRegion();
            UpdateItemsCollection(ItemsViewModelFiltered);
            OnPropertyChanged(nameof(ObservableCollectionCount));

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

        public async Task FetchItemsByText()
        {
            var newList = Items.FindAll(x => x.Name.ToLower().Contains(SearchText.Trim().ToLower()));
            ItemsViewModel = ConvertToViewModels(newList);
            UpdateItemsCollection(ItemsViewModel);
            OnPropertyChanged(nameof(ObservableCollectionCount));
        }

        public void SortCollection(string choosedOption)
        {
            switch (choosedOption)
            {
                case "Newest":
                    ItemsViewModelFiltered = ItemsViewModelFiltered.OrderByDescending(item => item.Item.UploadDate).ToList();
                    break;
                case "Oldest":
                    ItemsViewModelFiltered = ItemsViewModelFiltered.OrderBy(item => item.Item.UploadDate).ToList();
                    break;
                case "Highest Price":
                    ItemsViewModelFiltered = ItemsViewModelFiltered.OrderByDescending(item => item.PricePerDay).ToList();
                    break;
                case "Lowest Price":
                    ItemsViewModelFiltered = ItemsViewModelFiltered.OrderBy(item => item.PricePerDay).ToList();
                    break;
                case "Rating":
                    ItemsViewModelFiltered = ItemsViewModelFiltered.OrderByDescending(item => item.Item.ItemRating).ToList();
                    break;
            }
            UpdateItemsCollection(ItemsViewModelFiltered);
            OnPropertyChanged(nameof(ObservableCollectionCount));
        }





        public void FilterByCategory()
        {

            if(CategoryString == null)
            {
                CategoryString = "0";
            }

            switch (CategoryString)
            {
                case "0": //All
                    ItemsViewModelFiltered = ItemsViewModel;
                    break;
                case "1": // books
                    ItemsViewModelFiltered = ItemsViewModel.FindAll(item => item.Category == ECategories.Books.ToString()).ToList();
                    break;
                case "2": // music
                    ItemsViewModelFiltered = ItemsViewModel.FindAll(item => item.Category == ECategories.Music.ToString()).ToList();
                    break;
                case "3": // travel
                    ItemsViewModelFiltered = ItemsViewModel.FindAll(item => item.Category == ECategories.Travel.ToString()).ToList();
                    break;
                case "4": // clothing
                    ItemsViewModelFiltered = ItemsViewModel.FindAll(item => item.Category == ECategories.Clothing.ToString()).ToList();
                    break;
                case "5": // sport
                    ItemsViewModelFiltered = ItemsViewModel.FindAll(item => item.Category == ECategories.Sport.ToString()).ToList();
                    break;
                case "6": // electronics
                    ItemsViewModelFiltered = ItemsViewModel.FindAll(item => item.Category == ECategories.Electronics.ToString()).ToList();
                    break;
                case "7": // home
                    ItemsViewModelFiltered = ItemsViewModel.FindAll(item => item.Category == ECategories.Home.ToString()).ToList();
                    break;
                case "8": // bikes
                    ItemsViewModelFiltered = ItemsViewModel.FindAll(item => item.Category == ECategories.Bikes.ToString()).ToList();
                    break;
                case "9": // tools
                    ItemsViewModelFiltered = ItemsViewModel.FindAll(item => item.Category == ECategories.Tools.ToString()).ToList();
                    break;
            }
      
        }

        public void FilterByPriceRange()
        {
            ItemsViewModelFiltered = ItemsViewModelFiltered.FindAll(item => item.PricePerDay >= _searchPageStateService.PriceRangeStart && item.PricePerDay <= _searchPageStateService.PriceRangeEnd).ToList();
        }

        public void FilterByRegion()
        {
            if(_searchPageStateService.SelectedRegion != "All")
            {
                ItemsViewModelFiltered = ItemsViewModelFiltered.FindAll(item => item.Item.Region == _searchPageStateService.SelectedRegion).ToList();
            }
        }







        public Command FiltersButton_Tapped
        => new Command(async () => await Shell.Current.GoToAsync($"{nameof(FiltersPage)}"));

        public Command CategoriesButton_Tapped
       => new Command(async () => await Shell.Current.GoToAsync($"{nameof(CategoriesPage)}"));

    }
}