using Newtonsoft.Json;
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
        ItemReviewFrame.IsVisible = false;
        UserReviewFrame.IsVisible = false;
    }



    protected override async void OnAppearing()
    {
        TransactionPageViewModel ViewModel = (BindingContext as TransactionPageViewModel);
        base.OnAppearing();
        await ViewModel.deserializeString();

        //setting before images
        if (_userService.LoggedInUser.Id == ViewModel._transaction.ItemOwner)
        {
            BeforeConditionLabel.IsVisible = BeforeConditionGrid.IsVisible = ViewModel._transaction.OwnerImagesBefore != null;
            BeforeImage1.Source = ViewModel._transaction.OwnerImagesBefore?.Count >= 1 ?ViewModel._transaction.OwnerImagesBefore[0] : null;
            BeforeImage2.Source = ViewModel._transaction.OwnerImagesBefore?.Count >= 2 ? ViewModel._transaction.OwnerImagesBefore[1] : null;
            BeforeImage3.Source = ViewModel._transaction.OwnerImagesBefore?.Count >= 3 ? ViewModel._transaction.OwnerImagesBefore[2] : null;
            BeforeImage4.Source = ViewModel._transaction.OwnerImagesBefore?.Count >= 4 ? ViewModel._transaction.OwnerImagesBefore[3] : null;

            AfterConditionLabel.IsVisible = AfterConditionGrid.IsVisible = ViewModel._transaction.OwnerImagesAfter != null;
            AfterImage1.Source = ViewModel._transaction.OwnerImagesAfter?.Count >= 1 ? ViewModel._transaction.OwnerImagesAfter[0] : null;
            AfterImage2.Source = ViewModel._transaction.OwnerImagesAfter?.Count >= 2 ? ViewModel._transaction.OwnerImagesAfter[1] : null;
            AfterImage3.Source = ViewModel._transaction.OwnerImagesAfter?.Count >= 3 ? ViewModel._transaction.OwnerImagesAfter[2] : null;
            AfterImage4.Source = ViewModel._transaction.OwnerImagesAfter?.Count >= 4 ? ViewModel._transaction.OwnerImagesAfter[3] : null;


            UserReviewFrame.IsVisible = !(ViewModel._transaction.OwnerReviewedSeeker) && ViewModel._transaction.Status == enums.ETransactionStatus.Archived;

        }
        else
        {
            BeforeConditionLabel.IsVisible = BeforeConditionGrid.IsVisible = ViewModel._transaction.SeekerImagesBefore != null;
            BeforeImage1.Source = ViewModel._transaction.SeekerImagesBefore?.Count >= 1 ? ViewModel._transaction.SeekerImagesBefore[0] : null;
            BeforeImage2.Source = ViewModel._transaction.SeekerImagesBefore?.Count >= 2 ? ViewModel._transaction.SeekerImagesBefore[1] : null;
            BeforeImage3.Source = ViewModel._transaction.SeekerImagesBefore?.Count >= 3 ? ViewModel._transaction.SeekerImagesBefore[2] : null;
            BeforeImage4.Source = ViewModel._transaction.SeekerImagesBefore?.Count >= 4 ? ViewModel._transaction.SeekerImagesBefore[3] : null;

            AfterConditionLabel.IsVisible = AfterConditionGrid.IsVisible = ViewModel._transaction.SeekerImagesAfter != null;
            AfterImage1.Source = ViewModel._transaction.SeekerImagesAfter?.Count >= 1 ? ViewModel._transaction.SeekerImagesAfter[0] : null;
            AfterImage2.Source = ViewModel._transaction.SeekerImagesAfter?.Count >= 2 ? ViewModel._transaction.SeekerImagesAfter[1] : null;
            AfterImage3.Source = ViewModel._transaction.SeekerImagesAfter?.Count >= 3 ? ViewModel._transaction.SeekerImagesAfter[2] : null;
            AfterImage4.Source = ViewModel._transaction.SeekerImagesAfter?.Count >= 4 ? ViewModel._transaction.SeekerImagesAfter[3] : null;

            UserReviewFrame.IsVisible = !(ViewModel._transaction.SeekerReviewedOwner) && ViewModel._transaction.Status == enums.ETransactionStatus.Archived;
            ItemReviewFrame.IsVisible = !(ViewModel._transaction.SeekerReviewedItem) && ViewModel._transaction.Status == enums.ETransactionStatus.Archived;

        }

        //setting after image

        //if (_userService.LoggedInUser.Id == ViewModel._transaction.ItemOwner)
        //{
        //    AfterConditionLabel.IsVisible = AfterConditionGrid.IsVisible = ViewModel._transaction.OwnerImagesAfter != null;


        //    AfterImage1.Source = ViewModel._transaction.OwnerImagesAfter?.Count >= 1 ? ViewModel._transaction.OwnerImagesAfter[0] : null;
        //    AfterImage2.Source = ViewModel._transaction.OwnerImagesAfter?.Count >= 2 ? ViewModel._transaction.OwnerImagesAfter[1] : null;
        //    AfterImage3.Source = ViewModel._transaction.OwnerImagesAfter?.Count >= 3 ? ViewModel._transaction.OwnerImagesAfter[2] : null;
        //    AfterImage4.Source = ViewModel._transaction.OwnerImagesAfter?.Count >= 4 ? ViewModel._transaction.OwnerImagesAfter[3] : null;
        //}
        //else
        //{
        //    AfterConditionLabel.IsVisible = AfterConditionGrid.IsVisible = ViewModel._transaction.SeekerImagesAfter != null;

        //    AfterImage1.Source = ViewModel._transaction.SeekerImagesAfter?.Count >= 1 ? ViewModel._transaction.SeekerImagesAfter[0] : null;
        //    AfterImage2.Source = ViewModel._transaction.SeekerImagesAfter?.Count >= 2 ? ViewModel._transaction.SeekerImagesAfter[1] : null;
        //    AfterImage3.Source = ViewModel._transaction.SeekerImagesAfter?.Count >= 3 ? ViewModel._transaction.SeekerImagesAfter[2] : null;
        //    AfterImage4.Source = ViewModel._transaction.SeekerImagesAfter?.Count >= 4 ? ViewModel._transaction.SeekerImagesAfter[3] : null;
        //}
    }

    private async void ImageTapped(object sender, EventArgs e)
    {
        ImageButton Img = sender as ImageButton;
        var jsonStr = JsonConvert.SerializeObject(Img.Source);
        await Shell.Current.GoToAsync($"{nameof(PhotoDisplayPage)}?url={jsonStr}");
    }

    private async void UserReview_Sent(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert(" ", "Review sent.", "close");
        UserReviewFrame.IsVisible = false;
        await (BindingContext as TransactionPageViewModel).CreateUserReview();
    }

    private async void ItemReview_Sent(object sender, EventArgs e)
    {
        await Application.Current.MainPage.DisplayAlert(" ", "Review sent.", "close");
        ItemReviewFrame.IsVisible = false;
        await(BindingContext as TransactionPageViewModel).CreateItemReview();
    }
}