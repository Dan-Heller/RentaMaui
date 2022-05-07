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
  
    public class MyItemsPageViewModel
    {

        private List<Item> MyItems = new List<Item>(); 
        public ObservableRangeCollection<ItemViewModel> MyItemsCollection { get; private  set; } = new ObservableRangeCollection<ItemViewModel>();

        private List<ItemViewModel> MyItemsViewModel;

        private ItemService _itemService;

        public  MyItemsPageViewModel(ItemService itemService )
        {

            _itemService = itemService;
            //Task.Run(async () => await GetItems(itemService));
            
            // MyItems =  itemService.GetLoggedInUserItems();
        }

        //private async Task GetItems(ItemService itemService)
        //{
        //    MyItems = await itemService.GetLoggedInUserItems();
        //}


        internal async Task InitializeAsync()
        {
            await FetchAsync();
        }

        private async Task FetchAsync()
        {
            MyItems = await _itemService.GetLoggedInUserItems();

    
            //if (podcastsModels == null)
            //{
            //    await Shell.Current.DisplayAlert(
            //        AppResource.Error_Title,
            //        AppResource.Error_Message,
            //        AppResource.Close);

            //    return;
            //}

           
            MyItemsViewModel = ConvertToViewModels(MyItems);
            UpdateMyItemsCollection(MyItemsViewModel);

        }

        private  List<ItemViewModel> ConvertToViewModels(List<Item> Items)
        {
            var viewmodels = new List<ItemViewModel>();
            foreach (var item in Items)
            {
                var itemVM = new ItemViewModel(item);
                viewmodels.Add(itemVM);
            }
            return viewmodels;
        }

        private void UpdateMyItemsCollection(IEnumerable<ItemViewModel> myItemsVM)
        {
            MyItemsCollection.ReplaceRange(myItemsVM);
        }

        //Task CallAndForget()
        //{
        //    return 
        //}


    }
}
