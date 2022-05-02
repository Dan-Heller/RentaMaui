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

        public  MyItemsPageViewModel(ItemService itemService )
        {
            Task.Run(async () => await GetItems(itemService));

            // MyItems =  itemService.GetLoggedInUserItems();
        }

        private async Task GetItems(ItemService itemService)
        {
            MyItems = await itemService.GetLoggedInUserItems();

        }


        //Task CallAndForget()
        //{
        //    return 
        //}


    }
}
