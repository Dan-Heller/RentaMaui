using Renta.ViewModels;

namespace Renta;

public partial class MenuPage : ContentPage
{
	public MenuPage(MenuPageViewModel menuPageViewModel)
	{
        BindingContext = menuPageViewModel;
		InitializeComponent();
	}

    private void onSettingButtonClicked(object sender, EventArgs e)
    {
        

    }

    private void onRentingButtonClicked(object sender, EventArgs e)
    {

    }

    private void onMyItemsButtonClicked(object sender, EventArgs e)
    {

    }

    private void onRentalButtonClicked(object sender, EventArgs e)
    {

    }

    private void onWishListItemClicked(object sender, EventArgs e)
    {

    }
}