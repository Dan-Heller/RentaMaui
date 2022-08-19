using Microsoft.Extensions.DependencyInjection;
using Renta.Services;
using Renta.ViewModels;
using Renta.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.Reflection;
using Renta.Views;
using Syncfusion.Maui.Core.Hosting;

namespace Renta;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .ConfigureSyncfusionCore()
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

        builder.Services.AddTransient<ListChatPage>();
        builder.Services.AddTransient<ListChatPageViewModel>();
        builder.Services.AddSingleton<ChatViewModel>(); // delete this. only for test 

        builder.Services.AddSingleton<AppShell>(); 
		builder.Services.AddSingleton<AppShellViewModel>();

		builder.Services.AddSingleton<FeedPage>();
		builder.Services.AddTransient<FeedPageViewModel>();

		builder.Services.AddSingleton<ProfilePage>();
		builder.Services.AddSingleton<ProfilePageViewModel>();

		builder.Services.AddSingleton<UserService>();
		builder.Services.AddSingleton<FileService>();
		builder.Services.AddSingleton<ItemService>();
		builder.Services.AddSingleton<TransactionService>();
        builder.Services.AddSingleton<ReviewsService>();
        builder.Services.AddSingleton<SearchPageStateService>();

        builder.Services.AddTransient<LoginPage>();
		builder.Services.AddTransient<LoginPageViewModel>(); //maybe shouldnt be singleton ...

		builder.Services.AddTransient<RegistrationPage>();
		builder.Services.AddTransient<RegistrationPageViewModel>();

        
        builder.Services.AddTransient<MessagesPage>();
        builder.Services.AddTransient<MessagesPageViewModel>();

        builder.Services.AddSingleton<NotificationsPage>();
        builder.Services.AddSingleton<MessagesShell>();

		builder.Services.AddSingleton<EditProfilePage>();
		builder.Services.AddSingleton<EditProfilePageViewModel>();

        builder.Services.AddTransient<EditItemPage>();
        builder.Services.AddTransient<EditItemPageViewModel>();

        builder.Services.AddSingleton<SearchPage>();
		builder.Services.AddSingleton<SearchPageViewModel>();

		builder.Services.AddSingleton<CategoriesPage>();
        builder.Services.AddSingleton<FiltersPage>();
        builder.Services.AddSingleton<CategoriesPageViewModel>();
        builder.Services.AddSingleton<FilterPageViewModel>();


        builder.Services.AddSingleton<MenuPage>();
		builder.Services.AddSingleton<MenuPageViewModel>();

		builder.Services.AddSingleton<RentingPage>();
		builder.Services.AddSingleton<RentingPageViewModel>();

		builder.Services.AddSingleton<RentalPage>();
		builder.Services.AddSingleton<RentalPageViewModel>();

		builder.Services.AddSingleton<CreditsPage>();

		builder.Services.AddSingleton<MenuPage>();

        builder.Services.AddTransient<AddItemPage>();
        builder.Services.AddTransient<AddItemPageViewModel>();

		builder.Services.AddTransient<MyItemsPage>();
		builder.Services.AddTransient<MyItemsPageViewModel>();

		builder.Services.AddTransient<MyItemPage>();
		builder.Services.AddTransient<MyItemPageViewModel>();

		builder.Services.AddTransient<ItemPage>();
		builder.Services.AddTransient<ItemPageViewModel>();

		builder.Services.AddTransient<OtherUserProfilePage>();
		builder.Services.AddTransient<OtherUserProfilePageViewModel>();

        builder.Services.AddTransient<ActivateTransactionPage>();
        builder.Services.AddTransient<ActivateTransactionPageViewModel>();

        builder.Services.AddTransient<TransactionPage>();
        builder.Services.AddTransient<TransactionPageViewModel>();

        builder.Services.AddTransient<PhotoDisplayPage>();
        builder.Services.AddTransient<PhotoDisplayPageViewModel>();

        

        builder.Services.AddSingleton<SavedItemsPage>();
        builder.Services.AddSingleton<SavedItemsPageViewModel>();









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
