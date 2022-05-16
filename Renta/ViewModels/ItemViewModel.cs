using Newtonsoft.Json;
using Renta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public  class ItemViewModel
    {
        public Item Item { get; set; }

        public string? OwnerId { get => Item?.OwnerId; }

        public string? Id { get => Item?.Id; }
        public string? Name { get => Item?.Name; }

        public string? Category { get => Item?.Category; }

        public int? PricePerDay { get => Item?.PricePerDay; }

        public string? Description { get => Item?.Description.Trim(); }

        public List<string>? ImagesUrls { get => Item?.ImagesUrls; } 

        public List<DatesRange>? UnAvailableDates { get => Item?.UnAvailableDates; }

        public string FirstImageUrl { get { return ImagesUrls[0]; } }
       

        public ItemViewModel(Item item)
        {
            Item = item;
        }



        public Command GoToMyItemPage
      => new Command(async () => await MyItemCard_Tapped());

        private async Task MyItemCard_Tapped()
        {
            var jsonStr = JsonConvert.SerializeObject(Item);
            await Shell.Current.GoToAsync($"{nameof(MyItemPage)}?item={jsonStr}");
        }

       
        public Command GoToItemPage
    => new Command(async () => await ItemCard_Tapped());

        private async Task ItemCard_Tapped()
        {
            var jsonStr = JsonConvert.SerializeObject(Item);
            await Shell.Current.GoToAsync($"{nameof(ItemPage)}?item={jsonStr}");
        }

        public bool CheckIfRequestedDatesOverlapUnavailableDates(DateTime start, DateTime end)
        {
            bool IsOverlap = false;


            foreach(var range in UnAvailableDates)
            {
                if(!(start.CompareTo(range.EndDate) > 0 || end.CompareTo(range.StartDate) < 0))
                {
                    IsOverlap = true;
                    break;
                }
            }

            return IsOverlap;
        }


    }


}
