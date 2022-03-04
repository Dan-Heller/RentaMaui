using Autofac;
using Renta.Common.Navigation;
using System.Reflection;

namespace Renta;

public partial class App : Application
{
	public App(INavigationService navigationService)
	{
		InitializeComponent();

		MainPage = new NavigationPage();
		navigationService.NavigateToMainPage(MainPage.Navigation);

	}
}
