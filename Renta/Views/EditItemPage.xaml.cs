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
        string choosedOption = await DisplayActionSheet("Choose option:", "Cancel", null, "From Gallery", "Take a photo", "Remove image");
        var image = sender as ImageButton;
        var ImageId = image.ClassId;

        if (choosedOption == "From Gallery")
        {
            await (BindingContext as AddItemPageViewModel).AddPhotoFromGallery(ImageId);    //the of clicked -> init the method doesnt worked with onchanged
        }
        else if (choosedOption == "Take a photo")
        {
            await (BindingContext as AddItemPageViewModel).TakeAPhoto(ImageId);
        }
        else if (choosedOption == "Remove image")
        {
            (BindingContext as AddItemPageViewModel).RemoveImage(ImageId);
        }
    }

  
}