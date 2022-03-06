using Renta.Common.Navigation;
using Renta.ViewModels;
namespace Renta;


public partial class AppShell: Shell
{
	public AppShell(AppShellViewModel appShellViewModel)
	{ 
		BindingContext = appShellViewModel;
		InitializeComponent();

		Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));


		// FEED TAB
		//NavigationPage FeedTab = new NavigationPage(feedPage);
		//FeedTab.IconImageSource = "homeicon.png";
		//FeedTab.Title = "Home";
		//Children.Add(FeedTab);

	}
}

