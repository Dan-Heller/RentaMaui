using Microsoft.Extensions.DependencyInjection;
using Renta.Services;
using Renta.ViewModels;
using Renta.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Reflection;



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
		
        var a = Assembly.GetExecutingAssembly();  //all following rows are for the appsetting.json
        using var stream = a.GetManifestResourceStream("Renta.appsettings.json");
        var config = new ConfigurationBuilder()
                    .AddJsonStream(stream)
                    .Build();
        builder.Configuration.AddConfiguration(config);



        builder.Services.AddSingleton<AppShell>(); 
		builder.Services.AddSingleton<AppShellViewModel>();

		builder.Services.AddSingleton<FeedPage>();
		builder.Services.AddTransient<FeedPageViewModel>();

		builder.Services.AddSingleton<ProfilePage>();
		builder.Services.AddSingleton<ProfilePageViewModel>();

		builder.Services.AddSingleton<UserService>();
		builder.Services.AddSingleton<FileService>();
		builder.Services.AddSingleton<ItemService>();

		builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<LoginPageViewModel>(); //maybe shouldnt be singleton ...

		builder.Services.AddTransient<RegistrationPage>();
		builder.Services.AddTransient<RegistrationPageViewModel>();

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

		builder.Services.AddSingleton<RentingPage>();
		builder.Services.AddSingleton<RentalPage>();

		builder.Services.AddSingleton<CreditsPage>();

		builder.Services.AddSingleton<MenuPage>();

        builder.Services.AddTransient<AddItemPage>();
        builder.Services.AddTransient<AddItemPageViewModel>();

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
