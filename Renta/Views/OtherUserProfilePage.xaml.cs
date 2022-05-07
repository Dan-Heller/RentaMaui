using Renta.ViewModels;

namespace Renta;

public partial class OtherUserProfilePage : ContentPage
{

	private OtherUserProfilePageViewModel viewModel => BindingContext as OtherUserProfilePageViewModel;
	public OtherUserProfilePage(OtherUserProfilePageViewModel otherUserProfilePageViewModel)
	{
		BindingContext = otherUserProfilePageViewModel;
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await viewModel.InitializeAsync();

	}
}