using Newtonsoft.Json;
using Renta.Services;
using Renta.ViewModels;
namespace Renta;

public partial class CategoriesPage : ContentPage
{
    private SearchPageStateService _searchPageStateService;

	public CategoriesPage(SearchPageViewModel searchPageViewModel, SearchPageStateService searchPageStateService)
	{		
		BindingContext = searchPageViewModel;
        _searchPageStateService = searchPageStateService;
        InitializeComponent();
	}

    private void CategoryOption_Tapped(object sender, EventArgs e)
    {        
        _searchPageStateService._CategoryString = (sender as Frame).ClassId;        
        Shell.Current.GoToAsync("..");
    }  
}