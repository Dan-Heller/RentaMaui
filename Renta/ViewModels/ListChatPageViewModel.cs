using MvvmHelpers;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    public class ListChatPageViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private List<Chat> _chats;
        private List<ChatViewModel> _chatsViewModels;

        public ObservableRangeCollection<ChatViewModel> ChatsCollection { get; private set; } =
            new ObservableRangeCollection<ChatViewModel>();

        public ListChatPageViewModel(UserService userService)
        {
            _userService = userService;
            _chatsViewModels = new List<ChatViewModel>();
        }

        public async Task PageAppeared()
        {
            await _userService.UpdateLoggedInUser();
            _chats = _userService.LoggedInUser.PopulatedChats;

            _chatsViewModels = await ConvertToViewModels(_chats);
            UpdateMyItemsCollection(_chatsViewModels);
            OnPropertyChanged(nameof(ChatsCollection));
        }

        public void PageDisappeared()
        {
            _chatsViewModels.Clear();
            UpdateMyItemsCollection(_chatsViewModels);
            OnPropertyChanged(nameof(ChatsCollection));
        }

        private async Task<List<ChatViewModel>> ConvertToViewModels(List<Chat> chats)
        {
            var tasks = chats.Select(async (chat) =>
            {
                string otherUserId = _userService.LoggedInUser.Id == chat.UserA ? chat.UserB : chat.UserA;
                User otherUser = await _userService.GetUserById(otherUserId);
                var chatVM = new ChatViewModel(chat, _userService, otherUser);
                _chatsViewModels.Add(chatVM);
            });
            await Task.WhenAll(tasks);
            return _chatsViewModels;
        }


        private void UpdateMyItemsCollection(IEnumerable<ChatViewModel> ChatsVM)
        {
            ChatsCollection.ReplaceRange(ChatsVM);
        }
    }
}