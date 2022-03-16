using Renta.ViewModels;
namespace Renta;

public partial class EditProfilePage : ContentPage
{
	public EditProfilePage(EditProfilePageViewModel editProfilePageViewModel)
	{
		BindingContext = editProfilePageViewModel;

		InitializeComponent();
	}
}