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
		MainPage = appShell;
	}
	
}