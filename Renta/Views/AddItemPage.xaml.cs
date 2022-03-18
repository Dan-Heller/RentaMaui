using Renta.ViewModels;
namespace Renta;

public partial class AddItemPage : ContentPage
{
	public AddItemPage()
	{
		BindingContext = new AddItemPageViewModel();
		InitializeComponent();
	}

    private async void AddPhoto_Tapped(object sender, EventArgs e)
    {
		string choosedOption = await DisplayActionSheet("Choose option:", "Cancel", null, "From Gallery", "Take a photo");

		if(choosedOption == "From Gallery")
        {
			 await (BindingContext as AddItemPageViewModel).AddPhotoFromGallery();
		}
		else if(choosedOption == "Take a photo")
        {
			await (BindingContext as AddItemPageViewModel).TakeAPhoto();
		}
    }
}