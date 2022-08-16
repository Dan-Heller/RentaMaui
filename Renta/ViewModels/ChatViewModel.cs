using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class ChatViewModel
    {

        public ChatViewModel()
        {

        }




        public Command ListChatTapped
 => new Command(async () => await GoToChat());

        private async Task GoToChat()
        {
            //var jsonStr = JsonConvert.SerializeObject(Item);
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}");
        }
    }
}
