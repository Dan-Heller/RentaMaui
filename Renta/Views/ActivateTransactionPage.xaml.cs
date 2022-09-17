using Renta.ViewModels;

namespace Renta;

public partial class ActivateTransactionPage : ContentPage
{
    public ActivateTransactionPage(ActivateTransactionPageViewModel activateTransactionPageViewModel)
    {
        BindingContext = activateTransactionPageViewModel;
        InitializeComponent();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as ActivateTransactionPageViewModel).deserializeString();
    }

    private async void AddPhoto_Tapped(object sender, EventArgs e)
    {
        var image = sender as ImageButton;
        var imageId = image.ClassId;

        //check that previous slot was used 
        if (imageId != "1")
        {
            if (!((BindingContext as ActivateTransactionPageViewModel).chosenImagesFilesResult.Count >=
                  Int32.Parse(imageId) - 1))
            {
                return;
            }
        }

        string choosedOption = await DisplayActionSheet("Choose option:", "Cancel", null, "From Gallery",
            "Take a photo", "Remove image");

        if (choosedOption == "From Gallery")
        {
            await (BindingContext as ActivateTransactionPageViewModel)
                .AddPhotoFromGallery(imageId); //the of clicked -> init the method doesnt worked with onchanged
        }
        else if (choosedOption == "Take a photo")
        {
            await (BindingContext as ActivateTransactionPageViewModel).TakeAPhoto(imageId);
        }
        else if (choosedOption == "Remove image")
        {
            (BindingContext as ActivateTransactionPageViewModel).RemoveImage(imageId);
        }
    }
}