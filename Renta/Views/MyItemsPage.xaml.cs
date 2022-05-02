using Renta.ViewModels;
namespace Renta;

public partial class MyItemsPage : ContentPage
{
	public MyItemsPage(MyItemsPageViewModel myItemsPageViewModel)
	{
		BindingContext = myItemsPageViewModel;
		
		InitializeComponent();
	}
}