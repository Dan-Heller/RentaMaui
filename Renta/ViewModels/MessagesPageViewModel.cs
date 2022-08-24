using Renta.Models;
using MvvmHelpers;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Renta.ViewModels
{
    [QueryProperty(nameof(ChatString), "chat")]
    public class MessagesPageViewModel : BaseViewModel
    {

        private UserService _userService;
        public Chat _currentChat;
        public ChatViewModel _currentChatViewModel;
        private ChatService _chatService;
        private List<Message> messages;
        public ObservableRangeCollection<Message> MessagesCollection { get; private set; } = new ObservableRangeCollection<Message>();

        private string _ChatString;
        public String ChatString
        {
            get => _ChatString;
            set
            {
                _ChatString = Uri.UnescapeDataString(value ?? string.Empty);

            }
        }

        public async Task deserializeString()
        {
            _currentChat = JsonConvert.DeserializeObject<Chat>(_ChatString);

            User otherUser =  _userService.LoggedInUser.Id == _currentChat.UserA ? await _userService.GetUserById(_currentChat.UserB) : await _userService.GetUserById(_currentChat.UserA);

            _currentChatViewModel = new ChatViewModel(_currentChat, _userService, otherUser);
        }

        public MessagesPageViewModel(UserService userService, ChatService chatService)
        {
            _userService = userService;
           // _currentChat = currentChat;
            _chatService = chatService;
            _chatService.currentMessagesPage = this;

        }


        public void FetchMessagesFromChat()
        {
            deserializeString();
            messages = _currentChat.Messages;
            UpdateTransactionsCollection(messages);

        }

        public void AddMessageToCollection(Message newMessage)
        {
            messages.Add(newMessage);
            UpdateTransactionsCollection(messages);
            OnPropertyChanged(nameof(MessagesCollection));
        }


    

        private void UpdateTransactionsCollection(IEnumerable<Message> MessagesVM)
        {
            MessagesCollection.ReplaceRange(MessagesVM);
        }


    }
}
