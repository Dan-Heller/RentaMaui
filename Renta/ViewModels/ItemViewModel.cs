using Newtonsoft.Json;
using Renta.Models;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public  class ItemViewModel : BaseViewModel
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

        public string? UploadDate { get => Item.UploadDate.Value.Date.ToString("dd/MM/yyyy"); }

        public bool ItemLiked { get; set; } = false;

        public string FirstImageUrl { get { return ImagesUrls[0]; } }

        public int CurrentImageIndex { get; set; } = 0;

        public string CurrentImage { get { return ImagesUrls[CurrentImageIndex%ImagesUrls.Count]; } }

        private UserService _userService;

        

        public ImageSource HeartImageSource { get; set; }
        

        public ItemViewModel(Item item, UserService userService)
        {
            Item = item;
            _userService = userService;
            HeartImageSource = ImageSource.FromFile("hollowhearticon.png");

            //check if user already liked the item
            foreach (var likedItemId in _userService.LoggedInUser.LikedItems)
            {
                if(Id == likedItemId)
                {
                    ItemLiked = true;
                    HeartImageSource = ImageSource.FromFile("fullhearticon.png");
                }
            }
            OnPropertyChanged(nameof(HeartImageSource));

        }

        public Command HeartButtonClicked
=> new Command(async () => await UpdateLikedStatus());

        private async Task UpdateLikedStatus()
        {
            if (ItemLiked)
            {
                ItemLiked = false;
                _userService.LoggedInUser.LikedItems.Remove(Id);
                HeartImageSource = ImageSource.FromFile("hollowhearticon.png");
            }
            else
            {
                ItemLiked = true;
                _userService.LoggedInUser.LikedItems.Add(Id);
                HeartImageSource = ImageSource.FromFile("fullhearticon.png");
            }
            OnPropertyChanged(nameof(HeartImageSource));
            await _userService.UpdateUserInfo(_userService.LoggedInUser);
            await _userService.UpdateLoggedInUser();
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

        public async Task GotoMyUpdatedItemPage()
        {
            var jsonStr = JsonConvert.SerializeObject(Item);
            await Shell.Current.GoToAsync($"../../{nameof(MyItemPage)}?item={jsonStr}");
        }

        //public async Task GotoMyUpdatedItemPage()
        //{
        //    var jsonStr = JsonConvert.SerializeObject(Item);
        //    await Shell.Current.GoToAsync("../../..");
        //}

        public bool CheckIfRequestedDatesOverlapUnavailableDates(DateTime start, DateTime end)
        {
            bool isOverlap = false;
            
            foreach(var range in UnAvailableDates)
            {
                if(!(start.CompareTo(range.EndDate) > 0 || end.CompareTo(range.StartDate) < 0))
                {
                    isOverlap = true;
                    break;
                }
            }

            return isOverlap;
        }
    }


}
