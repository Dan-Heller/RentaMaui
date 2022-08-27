using Android.App;
using Android.Content.PM;
using Android.OS;
using Plugin.FirebasePushNotification;

namespace Renta;

[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]
public class MainActivity : MauiAppCompatActivity
{
    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        
        FirebasePushNotificationManager.ProcessIntent(this, Intent);
    }
}
