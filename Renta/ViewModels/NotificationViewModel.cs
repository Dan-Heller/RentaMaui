using Newtonsoft.Json;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    public class NotificationViewModel
    {
        private Notification Notification { get; set; }
        private readonly string _otherUserId;
        private readonly UserService _userService;
        private readonly User _otherUser;

        public ImageSource otherUserProfilePhoto
        {
            get => _otherUser.ProfilePhotoUrl;
        }

        public string OtherUserFullName
        {
            get => _otherUser.FirstName + " " + _otherUser.LastName;
        }

        public string NotificationTime { get; set; } = string.Empty;
        public string _message { get; set; } = string.Empty;

        public NotificationViewModel(Notification notification, UserService userService, User otherUser)
        {
            Notification = notification;
            _userService = userService;
            _otherUser = otherUser;
            _otherUserId = otherUser.Id;
            NotificationTime = notification.Time.ToString("HH:mm");
            insertNotificationMessage();
        }

        private void insertNotificationMessage()
        {
            if (Notification.NotificationType == enums.ENotificationType.Chat)
            {
                _message = "You recieved a message from " + OtherUserFullName;
            }
            else if (Notification.NotificationType == enums.ENotificationType.ItemRequest)
            {
                _message = OtherUserFullName + " is interested in your item.";
            }
            else
            {
                _message = OtherUserFullName + " approved your item request.";
            }
        }

        public Command NotificationTapped
            => new Command(async () => await GoToNextPage());

        private async Task GoToNextPage()
        {
            if (Notification.NotificationType == enums.ENotificationType.Chat)
            {
                Chat _chat = findChat();
                var jsonStr = JsonConvert.SerializeObject(_chat);
                await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?chat={jsonStr}");
            }
            else if (Notification.NotificationType == enums.ENotificationType.ItemRequest)
            {
                await Shell.Current.GoToAsync($"{nameof(RentingPage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"{nameof(RentalPage)}");
            }
        }

        private Chat findChat()
        {
            return _userService.LoggedInUser.PopulatedChats.Find(chat =>
                chat.UserA == _otherUserId || chat.UserB == _otherUserId);
        }
    }
}