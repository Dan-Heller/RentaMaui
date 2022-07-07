using MvvmHelpers;
using Newtonsoft.Json;
using Renta.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(MyItemString), "item")]
    public  class MyItemPageViewModel : ObservableObject
    {
        //private Item _myItem;
        public ItemViewModel _myItemViewModel { get; set; }
        
        private string _MyItemString;
        public String MyItemString
        {
            get => _MyItemString;
            set
            {
                _MyItemString = Uri.UnescapeDataString(value ?? string.Empty);
                
            }
        }

     
        public void deserializeString()
        {
            var Item = JsonConvert.DeserializeObject<Item>(_MyItemString);
            convertItemToViewModel(Item);
        }

        private void convertItemToViewModel(Item item)
        {
            _myItemViewModel = new ItemViewModel(item);
            OnPropertyChanged(nameof(_myItemViewModel));
        }

        public Command ProfileLink_Tapped
       => new Command(async () => await Shell.Current.GoToAsync($"{nameof(ProfilePage)}"));

   

        public Command EditButtonClicked
  => new Command(async () => await EditButton_Tapped());

        private async Task EditButton_Tapped()
        {
            var jsonStr = JsonConvert.SerializeObject(_myItemViewModel.Item);
            await Shell.Current.GoToAsync($"{nameof(EditItemPage)}?item={jsonStr}");
        }




      
    }
}
