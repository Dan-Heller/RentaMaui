using Autofac;

using System.Reflection;
using Android.OS;
using Plugin.FirebasePushNotification;

namespace Renta;

public partial class App : Application
{
	public App(AppShell appShell)
	{
		InitializeComponent();
		//MainPage = new NavigationPage();
		//navigationService.NavigateToMainPage(MainPage.Navigation);
		
		MainPage = appShell;

		CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
	}

	private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
	{
		System.Diagnostics.Debug.WriteLine($"blablabla token: {e.Token}");
	}
}