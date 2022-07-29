using Renta.Services;
using Renta.ViewModels;

namespace Renta.Views;

public partial class TransactionPage : ContentPage
{
    private UserService _userService;

	public TransactionPage(TransactionPageViewModel transactionPageViewModel, UserService userService)
	{
		BindingContext = transactionPageViewModel;
		InitializeComponent();
        _userService = userService;
        BeforeConditionGrid.IsVisible = false;
        BeforeConditionLabel.IsVisible = false;
        AfterConditionGrid.IsVisible = false;
        AfterConditionLabel.IsVisible = false;
    }



    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as TransactionPageViewModel).deserializeString();

        //setting before images
        if (_userService.LoggedInUser.Id == (BindingContext as TransactionPageViewModel)._transaction.ItemOwner)
        {
            BeforeConditionLabel.IsVisible = BeforeConditionGrid.IsVisible = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesBefore != null;

            
            BeforeImage1.Source = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesBefore?.Count >= 1 ?(BindingContext as TransactionPageViewModel)._transaction.OwnerImagesBefore[0] : null;
            BeforeImage2.Source = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesBefore?.Count >= 2 ? (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesBefore[1] : null;
            BeforeImage3.Source = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesBefore?.Count >= 3 ? (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesBefore[2] : null;
            BeforeImage4.Source = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesBefore?.Count >= 4 ? (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesBefore[3] : null;
        }
        else
        {
            BeforeConditionLabel.IsVisible = BeforeConditionGrid.IsVisible = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesBefore != null;

            BeforeImage1.Source = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesBefore?.Count >= 1 ? (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesBefore[0] : null;
            BeforeImage2.Source = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesBefore?.Count >= 2 ? (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesBefore[1] : null;
            BeforeImage3.Source = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesBefore?.Count >= 3 ? (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesBefore[2] : null;
            BeforeImage4.Source = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesBefore?.Count >= 4 ? (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesBefore[3] : null;
        }

        //setting after image

        if (_userService.LoggedInUser.Id == (BindingContext as TransactionPageViewModel)._transaction.ItemOwner)
        {
            AfterConditionLabel.IsVisible = AfterConditionGrid.IsVisible = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesAfter != null;


            AfterImage1.Source = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesAfter?.Count >= 1 ? (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesAfter[0] : null;
            AfterImage2.Source = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesAfter?.Count >= 2 ? (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesAfter[1] : null;
            AfterImage3.Source = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesAfter?.Count >= 3 ? (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesAfter[2] : null;
            AfterImage4.Source = (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesAfter?.Count >= 4 ? (BindingContext as TransactionPageViewModel)._transaction.OwnerImagesAfter[3] : null;
        }
        else
        {
            AfterConditionLabel.IsVisible = AfterConditionGrid.IsVisible = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesAfter != null;

            AfterImage1.Source = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesAfter?.Count >= 1 ? (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesAfter[0] : null;
            AfterImage2.Source = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesAfter?.Count >= 2 ? (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesAfter[1] : null;
            AfterImage3.Source = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesAfter?.Count >= 3 ? (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesAfter[2] : null;
            AfterImage4.Source = (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesAfter?.Count >= 4 ? (BindingContext as TransactionPageViewModel)._transaction.SeekerImagesAfter[3] : null;
        }

    }

}