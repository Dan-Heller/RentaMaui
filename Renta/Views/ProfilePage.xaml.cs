using Renta.Common.Navigation;
using Renta.ViewModels;

namespace Renta;

public partial class ProfilePage : ContentPage
{
	public ProfilePage(ProfilePageViewModel profilePageViewModel)
	{
		BindingContext = profilePageViewModel;
		//ProfilePageViewModel profilePageViewModel = new ProfilePageViewModel(navigationService);
		//BindingContext = profilePageViewModel;
		//profilePageViewModel.navigation = feedPage.Navigation;
		InitializeComponent();
	}
}