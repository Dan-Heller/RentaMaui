using Renta.ViewModels;

namespace Renta.Views;

public partial class PhotoDisplayPage : ContentPage
{
    public PhotoDisplayPage(PhotoDisplayPageViewModel photoDisplayPageViewModel)
    {
        BindingContext = photoDisplayPageViewModel;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as PhotoDisplayPageViewModel).ActivatePropertyChange();
    }
}