using Renta.Common.Navigation;
using Renta.ViewModels;
namespace Renta;


public partial class FeedPage : ContentPage
{
	public FeedPage(FeedPageViewModel feedPageViewModel)
	{
		//BindingContext = feedPageViewModel;
		//FeedPageViewModel feedPageViewModel = new FeedPageViewModel(navigationService);

		BindingContext = feedPageViewModel;
		feedPageViewModel.navigation = this.Navigation;  ////
		InitializeComponent();
		
	}
}