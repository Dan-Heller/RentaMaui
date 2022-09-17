using Android.App;
using Android.OS;
using Android.Runtime;
using Firebase.Messaging;
using Plugin.FirebasePushNotification;
using Renta.Services;

namespace Renta;

[Application]
public class MainApplication : MauiApplication
{
	public MainApplication(IntPtr handle, JniHandleOwnership ownership)
		: base(handle, ownership)
	{
		CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
	}
	
	private async void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
	{
		System.Diagnostics.Debug.WriteLine($"blablabla token: {e.Token}");
		await SecureStorage.Default.SetAsync("FCMToken", e.Token);
	}
	
	public override void OnCreate()
	{
		base.OnCreate();

		//Set the default notification channel for your app when running Android Oreo
		if (Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
		{
			//Change for your default notification channel id here
			FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

			//Change for your default notification channel name here
			FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
		}
 
		
		
		CrossFirebasePushNotification.Current.OnNotificationReceived += (s,p) =>
		{ 
			System.Diagnostics.Debug.WriteLine($"recived token: {p.Data}");
			Console.WriteLine($"recived token: {p}");
		}; 
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
