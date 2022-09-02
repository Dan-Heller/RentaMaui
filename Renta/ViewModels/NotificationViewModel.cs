
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
    public class NotificationViewModel
    {
        public Notification _notification { get; set; }


        
        public string OtherUserId;
        public UserService _userService;
        public User _otherUser;
        public ImageSource otherUserProfilePhoto { get => _otherUser.ProfilePhotoUrl; }
        public string OtherUserFullName { get => _otherUser.FirstName + " " + _otherUser.LastName; }
        
        public string NotificationTime { get; set; } = string.Empty;
        public string _message { get; set; } = string.Empty;
        public NotificationViewModel(Notification notification, UserService userService, User OtherUser)
        {
            _notification = notification;
            _userService = userService;
            _otherUser = OtherUser;
            OtherUserId = OtherUser.Id;
            insertNotificationMessage();

        }

        private void insertNotificationMessage()
        {
            if(_notification.NotificationType == enums.ENotificationType.Chat)
            {
                _message = "You recieved a message from" + OtherUserFullName;
            }
            else if(_notification.NotificationType == enums.ENotificationType.ItemRequest)
            {
                _message = OtherUserFullName + "is interested in your item.";
            }
            else
            {
                _message = OtherUserFullName + "approved your item request.";
            }
        }

        public Command NotificationTapped
=> new Command(async () => await GoToNextPage());

        private async Task GoToNextPage()
        {
            if (_notification.NotificationType == enums.ENotificationType.Chat)
            {
                Chat _chat = findChat();
                var jsonStr = JsonConvert.SerializeObject(_chat);
                await Shell.Current.GoToAsync($"{nameof(MessagesPage)}?chat={jsonStr}");
            }
            else if (_notification.NotificationType == enums.ENotificationType.ItemRequest)
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
            return _userService.LoggedInUser.PopulatedChats.Find(chat => chat.UserA == OtherUserId || chat.UserB == OtherUserId);
        }

    }

      
}
