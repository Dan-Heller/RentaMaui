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
}