using Renta.ViewModels;

namespace Renta.Views;

public partial class TransactionPage : ContentPage
{
	public TransactionPage(TransactionPageViewModel transactionPageViewModel)
	{
		BindingContext = transactionPageViewModel;
		InitializeComponent();
	}



    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as TransactionPageViewModel).deserializeString();

    }

}