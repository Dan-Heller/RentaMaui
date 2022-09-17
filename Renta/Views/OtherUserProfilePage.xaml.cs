using Renta.ViewModels;

namespace Renta;

public partial class OtherUserProfilePage : ContentPage
{
    private OtherUserProfilePageViewModel viewModel => BindingContext as OtherUserProfilePageViewModel;

    public OtherUserProfilePage(OtherUserProfilePageViewModel otherUserProfilePageViewModel)
    {
        BindingContext = otherUserProfilePageViewModel;

        InitializeComponent();
        ReviewsCollection.IsVisible = false;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.InitializeAsync();
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
        Items.TextColor = Color.FromHex("808080");
        Reviews.TextColor = Color.FromHex("808080");

        (sender as Label).TextColor = Color.FromHex("000000");

        if ((sender as Label) == Items)
        {
            ItemsCollection.IsVisible = true;
            ReviewsCollection.IsVisible = false;
        }
        else
        {
            ItemsCollection.IsVisible = false;
            ReviewsCollection.IsVisible = true;
        }
    }
}