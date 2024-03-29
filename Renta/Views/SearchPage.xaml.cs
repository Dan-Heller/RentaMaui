
using Renta.ViewModels;

namespace Renta;

public partial class SearchPage : ContentPage
{
	private SearchPageViewModel viewModel => BindingContext as SearchPageViewModel;

	public SearchPage(SearchPageViewModel searchPageViewModel)
	{
		BindingContext = searchPageViewModel;
		InitializeComponent();
	}

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await viewModel.InitializeAsync();

	}







	private async void SortByButton_Tapped(object sender, EventArgs e)
	{
		string choosedOption = await DisplayActionSheet("Sort By :",null, null, "Newest", "Oldest", "Highest Price", "Lowest Price", "Rating");
        (BindingContext as SearchPageViewModel).SortCollection(choosedOption);



       
	}

	private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
	{

		await (BindingContext as SearchPageViewModel).FetchItemsByText();

    }
}