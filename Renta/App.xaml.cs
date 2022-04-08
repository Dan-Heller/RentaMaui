using Autofac;

using System.Reflection;

namespace Renta;

public partial class App : Application
{
	public App(AppShell appShell)
	{
		InitializeComponent();
		//MainPage = new NavigationPage();
		//navigationService.NavigateToMainPage(MainPage.Navigation);
		

		MainPage = appShell;
	}
}