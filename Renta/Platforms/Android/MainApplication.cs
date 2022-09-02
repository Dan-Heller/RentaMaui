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

		// FirebaseMessaging.getInstance().getToken()
		// 	.addOnCompleteListener(new OnCompleteListener<String>() {
		// 		@Override
		// 		public void onComplete(@NonNull Task<String> task) {
		// 		if (!task.isSuccessful()) {
		// 		Log.w(TAG, "Fetching FCM registration token failed", task.getException());
		// 		return;
		// 	}
		//
		// // Get new FCM registration token
		// String token = task.getResult();
		//
		// // Log and toast
		// String msg = getString(R.string.msg_token_fmt, token);
		// Log.d(TAG, msg);
		// Toast.makeText(MainActivity.this, msg, Toast.LENGTH_SHORT).show();
		// }
		// });
		
		
		//Set the default notification channel for your app when running Android Oreo
		if (Build.VERSION.SdkInt >= global::Android.OS.BuildVersionCodes.O)
		{
			//Change for your default notification channel id here
			FirebasePushNotificationManager.DefaultNotificationChannelId = "FirebasePushNotificationChannel";

			//Change for your default notification channel name here
			FirebasePushNotificationManager.DefaultNotificationChannelName = "General";
		}
 
		//If debug you should reset the token each time.
		// #if DEBUG
		// FirebasePushNotificationManager.Initialize(this,false);
		// #else
		// FirebasePushNotificationManager.Initialize(this,false);
		// #endif

		//Handle notification when app is closed here
		CrossFirebasePushNotification.Current.OnNotificationReceived += (s,p) =>
		{ 
			System.Diagnostics.Debug.WriteLine($"recived token: {p.Data}");
			Console.WriteLine($"recived token: {p}");
		}; 
	}

	protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}
