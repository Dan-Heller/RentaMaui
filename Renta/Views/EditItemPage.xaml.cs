using Renta.ViewModels;

namespace Renta;

public partial class EditItemPage : ContentPage
{
    public EditItemPage(EditItemPageViewModel editItemPageViewModel)
    {
        BindingContext = editItemPageViewModel;

        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as EditItemPageViewModel).deserializeString();
    }

    private async void AddPhoto_Tapped(object sender, EventArgs e)
    {
        var image = sender as ImageButton;
        var ImageId = image.ClassId;

        //check that previous slot was used 
        if (ImageId != "1")
        {
            if ((BindingContext as EditItemPageViewModel).SlotHasImageArray[Int32.Parse(ImageId) - 2] == false)
            {
                return;
            }
        }

        string choosedOption = await DisplayActionSheet("Choose option:", "Cancel", null, "From Gallery",
            "Take a photo", "Remove image");

        if (choosedOption == "From Gallery")
        {
            await (BindingContext as EditItemPageViewModel)
                .AddPhotoFromGallery(ImageId); //the of clicked -> init the method doesnt worked with onchanged
        }
        else if (choosedOption == "Take a photo")
        {
            await (BindingContext as EditItemPageViewModel).TakeAPhoto(ImageId);
        }
        else if (choosedOption == "Remove image")
        {
            (BindingContext as EditItemPageViewModel).RemoveImage(ImageId);
        }
    }
}