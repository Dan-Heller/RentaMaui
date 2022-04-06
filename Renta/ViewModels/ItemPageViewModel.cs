using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public  class ItemPageViewModel
    {
        public bool ItemLiked;

        public ItemPageViewModel()
        {
            ItemLiked = false; //should be set according to the user liked items in data base/
        }

        

        public Command ProfileLink_Tapped
        => new Command(async () => await Shell.Current.GoToAsync($"{nameof(OtherUserProfilePage)}"));


    }
}
