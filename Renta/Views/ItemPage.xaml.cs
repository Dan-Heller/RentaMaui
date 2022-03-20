using Renta.ViewModels;
namespace Renta;

public partial class ItemPage : ContentPage
{
	public ItemPage()
	{
		BindingContext = new ItemPageViewModel();
		InitializeComponent();
	}
}