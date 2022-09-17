using Renta.ViewModels;

namespace Renta;

public partial class MyItemsPage : ContentPage
{
    private MyItemsPageViewModel viewModel => BindingContext as MyItemsPageViewModel;

    public MyItemsPage(MyItemsPageViewModel myItemsPageViewModel)
    {
        BindingContext = myItemsPageViewModel;
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.InitializeAsync();
    }
}