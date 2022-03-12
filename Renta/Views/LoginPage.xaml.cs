using Renta.ViewModels;
namespace Renta;


public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel loginPageViewModel)
	{

		BindingContext = loginPageViewModel;
		InitializeComponent();
		
	}
}