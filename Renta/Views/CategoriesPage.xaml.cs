using Newtonsoft.Json;
using Renta.Services;
using Renta.ViewModels;
namespace Renta;

public partial class CategoriesPage : ContentPage
{
    private SearchPageStateService _searchPageStateService;

	public CategoriesPage(SearchPageViewModel searchPageViewModel, SearchPageStateService searchPageStateService)
	{
		//BindingContext = categoriesPageViewModel;
		BindingContext = searchPageViewModel;
        _searchPageStateService = searchPageStateService;
        InitializeComponent();
	}

    private void CategoryOption_Tapped(object sender, EventArgs e)
    {
        //(BindingContext as CategoriesPageViewModel).CategoryOptionTappedGoBackToSearchPage.Execute(null);

        //(BindingContext as SearchPageViewModel).FilterByCategory(int.Parse((sender as Frame).ClassId));

        //var jsonStr = JsonConvert.SerializeObject((sender as Frame).ClassId);
        _searchPageStateService._CategoryString = (sender as Frame).ClassId;
        //Shell.Current.GoToAsync($"../?categoryStr={(sender as Frame).ClassId}");
        Shell.Current.GoToAsync("..");
    }

  
}