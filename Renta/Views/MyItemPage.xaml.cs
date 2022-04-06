using Renta.ViewModels;
namespace Renta;

public partial class MyItemPage : ContentPage
{
	
	public MyItemPage()
	{
		BindingContext = new ItemPageViewModel();
		InitializeComponent();

		
		
	}
}