using MvvmHelpers;
using Renta.Models;
using Renta.Services;

namespace Renta.ViewModels
{
    public class NotificationsPageViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private List<Notification> _notifications;
        private List<NotificationViewModel> _notificationsViewModels;

        public ObservableRangeCollection<NotificationViewModel> NotificationsCollection { get; private set; } =
            new ObservableRangeCollection<NotificationViewModel>();

        public NotificationsPageViewModel(UserService userService)
        {
            _userService = userService;
            _notificationsViewModels = new List<NotificationViewModel>();
        }

        public async Task PageAppeared()
        {
            await _userService.UpdateLoggedInUser();
            _notifications = _userService.LoggedInUser.Notifications;

            _notificationsViewModels = await ConvertToViewModels(_notifications);
            _notificationsViewModels = _notificationsViewModels
                .OrderByDescending(notification => notification.NotificationTime).ToList();
            UpdateMyNotifcationsCollection(_notificationsViewModels);
            OnPropertyChanged(nameof(NotificationsCollection));
        }

        public void PageDisappeared()
        {
            _notificationsViewModels.Clear();
            UpdateMyNotifcationsCollection(_notificationsViewModels);
            OnPropertyChanged(nameof(NotificationsCollection));
        }


        private async Task<List<NotificationViewModel>> ConvertToViewModels(List<Notification> notifications)
        {
            var tasks = notifications.Select(async (notification) =>
            {
                string otherUserId = notification.OtherUserId;
                User otherUser = await _userService.GetUserById(otherUserId);
                var notificationVM = new NotificationViewModel(notification, _userService, otherUser);
                _notificationsViewModels.Add(notificationVM);
            });
            await Task.WhenAll(tasks);
            return _notificationsViewModels;
        }

        private void UpdateMyNotifcationsCollection(IEnumerable<NotificationViewModel> notificationsVm)
        {
            NotificationsCollection.ReplaceRange(notificationsVm);
        }
    }
}