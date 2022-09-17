using Renta.ViewModels;

namespace Renta;

public partial class AddItemPage : ContentPage
{
    AddItemPageViewModel _addItemPageViewModel;

    public AddItemPage(AddItemPageViewModel addItemPageViewModel)
    {
        BindingContext = addItemPageViewModel;
        _addItemPageViewModel = addItemPageViewModel;
        InitializeComponent();
    }

    private async void AddPhoto_Tapped(object sender, EventArgs e)
    {
        var image = sender as ImageButton;
        var imageId = image.ClassId;

        //check that previous slot was used 
        if (imageId != "1")
        {
            if (!((BindingContext as AddItemPageViewModel).chosenImagesFilesResult.Count >= Int32.Parse(imageId) - 1))
            {
                return;
            }
        }

        string choosedOption = await DisplayActionSheet("Choose option:", "Cancel", null, "From Gallery",
            "Take a photo", "Remove image");

        if (choosedOption == "From Gallery")
        {
            await (BindingContext as AddItemPageViewModel)
                .AddPhotoFromGallery(imageId); //the of clicked -> init the method doesnt worked with onchanged
        }
        else if (choosedOption == "Take a photo")
        {
            await (BindingContext as AddItemPageViewModel).TakeAPhoto(imageId);
        }
        else if (choosedOption == "Remove image")
        {
            (BindingContext as AddItemPageViewModel).RemoveImage(imageId);
        }
    }
}