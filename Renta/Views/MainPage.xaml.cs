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
		NavigationPage navigation = new NavigationPage(feedPage);
		navigation.IconImageSource = "homeicon.png";
		navigation.Title = "Home";
		Children.Add(navigation);
		
		
 	}
}

