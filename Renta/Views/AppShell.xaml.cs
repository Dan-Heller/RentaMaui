
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
		Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
		Routing.RegisterRoute(nameof(AddItemPage), typeof(AddItemPage));
		Routing.RegisterRoute(nameof(ItemPage), typeof(ItemPage));
		Routing.RegisterRoute(nameof(FiltersPage), typeof(FiltersPage));
		Routing.RegisterRoute(nameof(CategoriesPage), typeof(CategoriesPage));
		Routing.RegisterRoute(nameof(OtherUserProfilePage), typeof(OtherUserProfilePage));
		Routing.RegisterRoute(nameof(MyItemsPage), typeof(MyItemsPage));
		Routing.RegisterRoute(nameof(MyItemPage), typeof(MyItemPage));
		Routing.RegisterRoute(nameof(SavedItemsPage), typeof(SavedItemsPage));
		Routing.RegisterRoute(nameof(RentingPage), typeof(RentingPage));
		Routing.RegisterRoute(nameof(RentalPage), typeof(RentalPage));
        Routing.RegisterRoute(nameof(EditItemPage), typeof(EditItemPage));
        Routing.RegisterRoute(nameof(NotificationsPage), typeof(NotificationsPage));
        Routing.RegisterRoute(nameof(ActivateTransactionPage), typeof(ActivateTransactionPage));


        //      Routing.RegisterRoute(nameof(MessagesShell), typeof(MessagesShell));
        //Routing.RegisterRoute(nameof(MessagesPage), typeof(MessagesPage));


    }
}

