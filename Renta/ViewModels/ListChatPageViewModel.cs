using MvvmHelpers;
using Renta.Models;
using Renta.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Renta.ViewModels
{
    public class ListChatPageViewModel : BaseViewModel
    {
        public UserService _userService;
        public List<Chat> chats;
        public List<ChatViewModel> chatsViewModels;
        public ObservableRangeCollection<ChatViewModel> ChatsCollection { get; private set; } = new ObservableRangeCollection<ChatViewModel>();

        public ListChatPageViewModel(UserService userService)
        {
            _userService = userService;
            chatsViewModels = new List<ChatViewModel>();
        }

        public async Task PageAppeared()
        {
            await _userService.UpdateLoggedInUser();
            chats = _userService.LoggedInUser.PopulatedChats;

            chatsViewModels = await ConvertToViewModels(chats);
            UpdateMyItemsCollection(chatsViewModels);
            OnPropertyChanged(nameof(ChatsCollection));
        }

        public void PageDisappeared()
        {
            chatsViewModels.Clear();
            UpdateMyItemsCollection(chatsViewModels);
            OnPropertyChanged(nameof(ChatsCollection));
        }


        private async Task<List<ChatViewModel>> ConvertToViewModels(List<Chat> Chats)
        {
            //var viewmodels = new List<ChatViewModel>();
            //foreach (var chat in Chats)
            //{
            //    var chatVM = new ChatViewModel(chat, _userService);
            //    chatsViewModels.Add(chatVM);
            //}
            //return chatsViewModels;


            var tasks = Chats.Select(async (chat) => {
                string OtherUserId = _userService.LoggedInUser.Id == chat.UserA ? chat.UserB : chat.UserA;
                User otherUser = await _userService.GetUserById(OtherUserId);
                var chatVM = new ChatViewModel(chat, _userService, otherUser);
                chatsViewModels.Add(chatVM);

            });
            await Task.WhenAll(tasks);
            return chatsViewModels;
        }



        private void UpdateMyItemsCollection(IEnumerable<ChatViewModel> ChatsVM)
        {
            ChatsCollection.ReplaceRange(ChatsVM);
        }


    }

 

}
