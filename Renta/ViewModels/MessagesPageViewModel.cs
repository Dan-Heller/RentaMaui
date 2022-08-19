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
    [QueryProperty(nameof(ChatString), "chatvm")]
    public class MessagesPageViewModel : BaseViewModel
    {

        private UserService _userService;
        public ChatViewModel _currentChat;
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

        public void deserializeString()
        {
            _currentChat = JsonConvert.DeserializeObject<ChatViewModel>(_ChatString);
           
        }

        public MessagesPageViewModel(UserService userService, ChatViewModel currentChat, ChatService chatService)
        {
            _userService = userService;
            _currentChat = currentChat;
            _chatService = chatService;
            _chatService.currentMessagesPage = this;

        }


        public void FetchMessagesFromChat()
        {
            deserializeString();
            messages = _currentChat._chat.Messages;
            UpdateTransactionsCollection(messages);

        }

        public void AddMessageToCollection(Message newMessage)
        {
            messages.Add(newMessage);
            UpdateTransactionsCollection(messages);
            OnPropertyChanged("MessagesCollection");
        }


    

        private void UpdateTransactionsCollection(IEnumerable<Message> MessagesVM)
        {
            MessagesCollection.ReplaceRange(MessagesVM);
        }


    }
}
