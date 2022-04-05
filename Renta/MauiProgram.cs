using Microsoft.Extensions.DependencyInjection;
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

		builder.Services.AddSingleton<AppShell>(); 
		builder.Services.AddSingleton<AppShellViewModel>();

		builder.Services.AddSingleton<FeedPage>();
		builder.Services.AddTransient<FeedPageViewModel>();

		builder.Services.AddSingleton<ProfilePage>();
		builder.Services.AddTransient<ProfilePageViewModel>();

		builder.Services.AddSingleton<LoginPage>();
		builder.Services.AddTransient<LoginPageViewModel>();

		builder.Services.AddSingleton<MessagesPage>();
		builder.Services.AddSingleton<MessagesShell>();

		builder.Services.AddSingleton<EditProfilePage>();
		builder.Services.AddSingleton<EditProfilePageViewModel>();

		builder.Services.AddSingleton<SearchPage>();
		builder.Services.AddSingleton<SearchPageViewModel>();

		builder.Services.AddSingleton<CategoriesPage>();
        builder.Services.AddSingleton<CategoriesPageViewModel>();

		builder.Services.AddSingleton<MenuPage>();
		builder.Services.AddSingleton<MenuPageViewModel>();




		builder.Services.AddSingleton<CreditsPage>();

		builder.Services.AddSingleton<MenuPage>();

		//builder.Services.AddSingleton<AddItemPage>();
		//builder.Services.AddSingleton<AddItemPageViewModel>();

		builder.Services.AddSingleton<SearchPage>();

		builder.Services.AddSingleton<NotificationsPage>();

		



		//builder.Services.AddTransient<AppShell>();  ///???
		//builder.Services.AddTransient<AppShellViewModel>();

		//      builder.Services.AddTransient<FeedPage>();
		//      builder.Services.AddTransient<FeedPageViewModel>();

		//      builder.Services.AddTransient<ProfilePage>();
		//      builder.Services.AddTransient<ProfilePageViewModel>();

		//      builder.Services.AddSingleton<INavigationService, NavigationService>();



		return builder.Build();
	}
}
