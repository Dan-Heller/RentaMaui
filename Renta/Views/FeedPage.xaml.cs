
//using AndroidX.Lifecycle;
using Renta.ViewModels;
namespace Renta;

public partial class FeedPage : ContentPage
{
	public string GreetingUser = String.Empty;

	public FeedPage(FeedPageViewModel feedPageViewModel)
	{
		BindingContext = feedPageViewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
		base.OnAppearing();
        GreetingUser = "Hello " + (BindingContext as FeedPageViewModel)._userService.LoggedInUser.FirstName;
        GreetingsLabel.Text = GreetingUser;
		await (BindingContext as FeedPageViewModel).InitializeAsync();
    }
}