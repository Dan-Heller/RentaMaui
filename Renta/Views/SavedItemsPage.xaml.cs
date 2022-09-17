using Renta.ViewModels;

namespace Renta;

public partial class SavedItemsPage : ContentPage
{
	public SavedItemsPage(SavedItemsPageViewModel savedItemsPageViewModel)
	{
        BindingContext = savedItemsPageViewModel;
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as SavedItemsPageViewModel).InitializeAsync();
    }
}