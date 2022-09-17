using Renta.ViewModels;

namespace Renta;

public partial class ListChatPage : ContentPage
{
    public ListChatPage(ListChatPageViewModel listChatPageViewModel)
    {
        BindingContext = listChatPageViewModel;
        InitializeComponent();
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as ListChatPageViewModel).PageAppeared();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        (BindingContext as ListChatPageViewModel).PageDisappeared();
    }
}