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
    public class NotificationsPageViewModel : BaseViewModel
    {
        public UserService _userService;
        public List<Notification> notifications;
        public List<NotificationViewModel> notificationsViewModels;
        public ObservableRangeCollection<NotificationViewModel> NotificationsCollection { get; private set; } = new ObservableRangeCollection<NotificationViewModel>();

        public NotificationsPageViewModel(UserService userService)
        {
            _userService = userService;
            notificationsViewModels = new List<NotificationViewModel>();
        }

        public async Task PageAppeared()
        {
            await _userService.UpdateLoggedInUser();
            notifications = _userService.LoggedInUser.Notifications;

            notificationsViewModels = await ConvertToViewModels(notifications);
            notificationsViewModels = notificationsViewModels.OrderByDescending(notification => notification.NotificationTime).ToList();
            UpdateMyNotifcationsCollection(notificationsViewModels);
            OnPropertyChanged(nameof(NotificationsCollection));
        }

        public void PageDisappeared()
        {
            notificationsViewModels.Clear();
            UpdateMyNotifcationsCollection(notificationsViewModels);
            OnPropertyChanged(nameof(NotificationsCollection));
        }


        private async Task<List<NotificationViewModel>> ConvertToViewModels(List<Notification> Notifications)
        {
          

            var tasks = Notifications.Select(async (notification) => {
                string OtherUserId = notification.OtherUserId ;
                User otherUser = await _userService.GetUserById(OtherUserId);
                var notificationVM = new NotificationViewModel(notification, _userService, otherUser);
                notificationsViewModels.Add(notificationVM);

            });
            await Task.WhenAll(tasks);
            return notificationsViewModels;
        }



        private void UpdateMyNotifcationsCollection(IEnumerable<NotificationViewModel> NotificationsVM)
        {
            NotificationsCollection.ReplaceRange(NotificationsVM);
           
        }


    }


}
