
using Renta.Common.Navigation;
using Renta.ViewModels;

namespace Renta;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
			});

		builder.Services.AddTransient<MainPage>();  ///???
		builder.Services.AddTransient<MainPageViewModel>();

        builder.Services.AddTransient<FeedPage>();
        builder.Services.AddTransient<FeedPageViewModel>();

        builder.Services.AddTransient<ProfilePage>();
        builder.Services.AddTransient<ProfilePageViewModel>();





        builder.Services.AddSingleton<INavigationService, NavigationService>();



		return builder.Build();
	}
}
