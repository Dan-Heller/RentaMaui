
using Renta.ViewModels;

namespace Renta;

public partial class SearchPage : ContentPage
{
	public SearchPage(SearchPageViewModel searchPageViewModel)
	{
		BindingContext = searchPageViewModel;
		InitializeComponent();
	}


	private async void SortByButton_Tapped(object sender, EventArgs e)
	{
		string choosedOption = await DisplayActionSheet("Sort By :",null, null, "Newest", "Oldest", "Highest Price", "Lowest Price");

		//if (choosedOption == "From Gallery")
		//{
		//	await (BindingContext as AddItemPageViewModel).AddPhotoFromGallery();
		//}
		//else if (choosedOption == "Take a photo")
		//{
		//	await (BindingContext as AddItemPageViewModel).TakeAPhoto();
		//}
	}
}