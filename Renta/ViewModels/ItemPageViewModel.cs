using Newtonsoft.Json;
using Renta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(ItemString), "item")]
    public  class ItemPageViewModel : BaseViewModel
    {
        public bool ItemLiked;
        public ItemViewModel _ItemViewModel { get; set; }

        private string _ItemString;
        public String ItemString
        {
            get => _ItemString;
            set
            {
                _ItemString = Uri.UnescapeDataString(value ?? string.Empty);

            }
        }


        public void deserializeString()
        {
            var Item = JsonConvert.DeserializeObject<Item>(_ItemString);
            convertItemToViewModel(Item);
        }

        private void convertItemToViewModel(Item item)
        {
            _ItemViewModel = new ItemViewModel(item);
            OnPropertyChanged(nameof(_ItemViewModel));
        }


        public ItemPageViewModel()
        {
            ItemLiked = false; //should be set according to the user liked items in data base/
        }

       
        public Command ProfileLink_Tapped
        => new Command(async () => await Shell.Current.GoToAsync($"{nameof(OtherUserProfilePage)}?OwnerId={_ItemViewModel.OwnerId}"));


    }
}
