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
        if (_userService.LoggedInUser.Id == ViewModel.Transaction.ItemOwner)
        {
            BeforeConditionLabel.IsVisible = BeforeConditionGrid.IsVisible = ViewModel.Transaction.OwnerImagesBefore != null;
            BeforeImage1.Source = ViewModel.Transaction.OwnerImagesBefore?.Count >= 1 ? ViewModel.Transaction.OwnerImagesBefore[0] : null;
            BeforeImage2.Source = ViewModel.Transaction.OwnerImagesBefore?.Count >= 2 ? ViewModel.Transaction.OwnerImagesBefore[1] : null;
            BeforeImage3.Source = ViewModel.Transaction.OwnerImagesBefore?.Count >= 3 ? ViewModel.Transaction.OwnerImagesBefore[2] : null;
            BeforeImage4.Source = ViewModel.Transaction.OwnerImagesBefore?.Count >= 4 ? ViewModel.Transaction.OwnerImagesBefore[3] : null;

            AfterConditionLabel.IsVisible = AfterConditionGrid.IsVisible = ViewModel.Transaction.OwnerImagesAfter != null;
            AfterImage1.Source = ViewModel.Transaction.OwnerImagesAfter?.Count >= 1 ? ViewModel.Transaction.OwnerImagesAfter[0] : null;
            AfterImage2.Source = ViewModel.Transaction.OwnerImagesAfter?.Count >= 2 ? ViewModel.Transaction.OwnerImagesAfter[1] : null;
            AfterImage3.Source = ViewModel.Transaction.OwnerImagesAfter?.Count >= 3 ? ViewModel.Transaction.OwnerImagesAfter[2] : null;
            AfterImage4.Source = ViewModel.Transaction.OwnerImagesAfter?.Count >= 4 ? ViewModel.Transaction.OwnerImagesAfter[3] : null;


            UserReviewFrame.IsVisible = !(ViewModel.Transaction.OwnerReviewedSeeker) && ViewModel.Transaction.Status == enums.ETransactionStatus.Archived;
        }
        else
        {
            BeforeConditionLabel.IsVisible = BeforeConditionGrid.IsVisible = ViewModel.Transaction.SeekerImagesBefore != null;
            BeforeImage1.Source = ViewModel.Transaction.SeekerImagesBefore?.Count >= 1 ? ViewModel.Transaction.SeekerImagesBefore[0] : null;
            BeforeImage2.Source = ViewModel.Transaction.SeekerImagesBefore?.Count >= 2 ? ViewModel.Transaction.SeekerImagesBefore[1] : null;
            BeforeImage3.Source = ViewModel.Transaction.SeekerImagesBefore?.Count >= 3 ? ViewModel.Transaction.SeekerImagesBefore[2] : null;
            BeforeImage4.Source = ViewModel.Transaction.SeekerImagesBefore?.Count >= 4 ? ViewModel.Transaction.SeekerImagesBefore[3] : null;

            AfterConditionLabel.IsVisible = AfterConditionGrid.IsVisible = ViewModel.Transaction.SeekerImagesAfter != null;
            AfterImage1.Source = ViewModel.Transaction.SeekerImagesAfter?.Count >= 1 ? ViewModel.Transaction.SeekerImagesAfter[0] : null;
            AfterImage2.Source = ViewModel.Transaction.SeekerImagesAfter?.Count >= 2 ? ViewModel.Transaction.SeekerImagesAfter[1] : null;
            AfterImage3.Source = ViewModel.Transaction.SeekerImagesAfter?.Count >= 3 ? ViewModel.Transaction.SeekerImagesAfter[2] : null;
            AfterImage4.Source = ViewModel.Transaction.SeekerImagesAfter?.Count >= 4 ? ViewModel.Transaction.SeekerImagesAfter[3] : null;

            UserReviewFrame.IsVisible = !(ViewModel.Transaction.SeekerReviewedOwner) && ViewModel.Transaction.Status == enums.ETransactionStatus.Archived;
            ItemReviewFrame.IsVisible = !(ViewModel.Transaction.SeekerReviewedItem) && ViewModel.Transaction.Status == enums.ETransactionStatus.Archived;
        }
    }

    private async void ImageTapped(object sender, EventArgs e)
    {
        ImageButton img = sender as ImageButton;
        var jsonStr = JsonConvert.SerializeObject(img.Source);
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