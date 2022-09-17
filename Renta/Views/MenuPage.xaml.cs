using Renta.ViewModels;

namespace Renta;

public partial class MenuPage : ContentPage
{
	public MenuPage(MenuPageViewModel menuPageViewModel)
	{
        BindingContext = menuPageViewModel;
		InitializeComponent();
	}
}