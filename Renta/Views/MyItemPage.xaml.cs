using Renta.ViewModels;
namespace Renta;

public partial class MyItemPage : ContentPage
{	
	public MyItemPage(MyItemPageViewModel myItemPageViewModel)
	{
		BindingContext = myItemPageViewModel;
		InitializeComponent();				
	}


	protected async override void OnAppearing()
	{
		base.OnAppearing();
		(BindingContext as MyItemPageViewModel).deserializeString();
        await(BindingContext as MyItemPageViewModel).FetchItemReviews();
    }   
}