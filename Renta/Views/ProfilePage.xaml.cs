
using Renta.ViewModels;

namespace Renta;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel profilePageViewModel)
	{
		BindingContext = profilePageViewModel;
		
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
       await  (BindingContext as ProfilePageViewModel).UpdateUserInfo();

    }


}