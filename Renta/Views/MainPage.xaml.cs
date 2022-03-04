using Renta.Common.Navigation;
using Renta.ViewModels;
namespace Renta;


public partial class MainPage : TabbedPage
{
	public MainPage(MainPageViewModel viewModel,FeedPage feedPage)

	{ 
		BindingContext = viewModel;
		InitializeComponent();

		// FEED TAB
		NavigationPage FeedTab = new NavigationPage(feedPage);
		FeedTab.IconImageSource = "homeicon.png";
		FeedTab.Title = "Home";
		Children.Add(FeedTab);

	}
}

