using Renta.ViewModels;
namespace Renta;


public partial class FeedPage : ContentPage
{
	public FeedPage()
	{
		InitializeComponent();
		BindingContext = new FeedPageViewModel(Navigation);
	}

   
}