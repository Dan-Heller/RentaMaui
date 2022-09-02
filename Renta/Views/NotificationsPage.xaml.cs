using Renta.ViewModels;

namespace Renta;

public partial class NotificationsPage : ContentPage
{
	public NotificationsPage(NotificationsPageViewModel notificationsPageViewModel)
	{
		BindingContext = notificationsPageViewModel;
		InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as NotificationsPageViewModel).PageAppeared();

    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as NotificationsPageViewModel).PageDisappeared();

    }
}