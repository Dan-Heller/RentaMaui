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
		string choosedOption = await DisplayActionSheet("Choose option:", "Cancel", null, "From Gallery", "Take a photo");
		var image = sender as ImageButton;
		var ImageId = image.ClassId;

		if(choosedOption == "From Gallery")
        {
			await (BindingContext as AddItemPageViewModel).AddPhotoFromGallery();    //the of clicked -> init the method doesnt worked with onchanged
			//await _addItemPageViewModel.AddPhotoFromGallery();
			//(BindingContext as AddItemPageViewModel).AddPhotoFromGallery_Clicked.Execute(null);
			//await _addItemPageViewModel.AddPhotoFromGallery();
			//_addItemPageViewModel.AddPhotoFromGallery_Clicked.Execute(null);

			//ImageSource1 = sourceResult;
			//OnPropertyChanged(nameof(ImageSource1));

		}
		else if(choosedOption == "Take a photo")
        {
			await (BindingContext as AddItemPageViewModel).TakeAPhoto(ImageId);
		}
    }
}