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
		Routing.RegisterRoute(nameof(CreditsPage), typeof(CreditsPage));
		Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));



		// FEED TAB
		//NavigationPage FeedTab = new NavigationPage(feedPage);
		//FeedTab.IconImageSource = "homeicon.png";
		//FeedTab.Title = "Home";
		//Children.Add(FeedTab);

	}
}

