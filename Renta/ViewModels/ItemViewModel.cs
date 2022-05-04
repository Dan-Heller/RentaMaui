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

        public string? Description { get => Item?.Description; }

        public List<string>? ImagesUrls { get => Item?.ImagesUrls; } 

        public List<DateTime>? UnAvailableDates { get => Item?.UnAvailableDates; }

        public string FirstImageUrl { get { return ImagesUrls[0]; } }
       

        public ItemViewModel(Item item)
        {
            Item = item;
        }

        //private Task NavigateToMyItemPage()
        //{
        //    return Shell.Current.GoToAsync($"{nameof(ShowDetailPage)}?Id={Show.Id}");
        //}


    }
}
