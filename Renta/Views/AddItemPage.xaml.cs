using Renta.ViewModels;
namespace Renta;

public partial class AddItemPage : ContentPage
{
	AddItemPageViewModel _addItemPageViewModel;
	
	public AddItemPage(AddItemPageViewModel addItemPageViewModel)
	{
		//BindingContext = new AddItemPageViewModel();
		BindingContext = addItemPageViewModel;
		 _addItemPageViewModel = addItemPageViewModel;
		InitializeComponent();
        

    }

    private async void AddPhoto_Tapped(object sender, EventArgs e)
    {
		
		var image = sender as ImageButton;
		var ImageId = image.ClassId;

        //check that previous slot was used 
        if (ImageId != "1")
        {
            if (!((BindingContext as AddItemPageViewModel).chosenImagesFilesResult.Count >= Int32.Parse(ImageId) - 1 ))
            {
                return;
            }
        }

        string choosedOption = await DisplayActionSheet("Choose option:", "Cancel", null, "From Gallery", "Take a photo", "Remove image");

        if (choosedOption == "From Gallery")
        {
			await (BindingContext as AddItemPageViewModel).AddPhotoFromGallery(ImageId);    //the of clicked -> init the method doesnt worked with onchanged
		}
		else if(choosedOption == "Take a photo")
        {
			await (BindingContext as AddItemPageViewModel).TakeAPhoto(ImageId);
		}
		else if(choosedOption == "Remove image")
        {
            (BindingContext as AddItemPageViewModel).RemoveImage(ImageId);
        }
    }
}