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
        public string LastMessage { get; set; }
        public string LastMessageTime { get; set; }

        public ChatViewModel(Chat chat, UserService userService, User OtherUser)
        {
            _chat = chat;
            _userService = userService;
            _otherUser = OtherUser;
            LastMessage = _chat.Messages.LastOrDefault().Text;
            LastMessageTime = _chat.Messages.LastOrDefault().Time.ToString("HH:mm");
        }




        public Command ListChatTapped
 => new Command(async () => await GoToChat());

        private async Task GoToChat()
        {
            var jsonStr = JsonConvert.SerializeObject(this);
            await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?chatvm={jsonStr}");
        }
    }
}
