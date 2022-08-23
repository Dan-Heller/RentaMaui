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
    public class ChatViewModel
    {

        public Chat _chat;
        public string OtherUserId;
        public UserService _userService;
        public User _otherUser;
        public ImageSource otherUserProfilePhoto { get => _otherUser.ProfilePhotoUrl  ;  }
        public string OtherUserFullName { get => _otherUser.FirstName + " " + _otherUser.LastName;   }
        public string LastMessage { get; set; } = string.Empty;
        public string LastMessageTime { get; set; } = string.Empty;

        public ChatViewModel(Chat chat, UserService userService, User OtherUser)
        {
            _chat = chat;
            _userService = userService;
            _otherUser = OtherUser;
            OtherUserId = OtherUser.Id;
            if (chat.Messages.Count > 0)
            {
                LastMessage = _chat.Messages.LastOrDefault().Text;
                LastMessageTime = _chat.Messages.LastOrDefault().Time.ToString("HH:mm");
            }
           
        }




        public Command ListChatTapped
 => new Command(async () => await GoToChat());

        private async Task GoToChat()
        {
            var jsonStr = JsonConvert.SerializeObject(_chat);
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?chat={jsonStr}");
        }
    }
}
