using Renta.Dto_s;
using Renta.Services;
using Renta.ViewModels;
namespace Renta;


public partial class LoginPage : ContentPage
{
    public UserService _userService;

	public LoginPage(LoginPageViewModel loginPageViewModel,UserService userService)
	{

		BindingContext = loginPageViewModel;
        _userService = userService;
		InitializeComponent();
		
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if(DeviceInfo.Current.Platform == DevicePlatform.Android)
		{
			string authToken = await SecureStorage.GetAsync("AuthToken");
			if(authToken is not null)
			{
				await _userService.GetUserFromToken(authToken);
				await Shell.Current.GoToAsync($"//{nameof(FeedPage)}");
			}
		}
    }
}