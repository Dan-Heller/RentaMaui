using Newtonsoft.Json;
using Renta.ViewModels;
namespace Renta;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(SearchPageViewModel searchPageViewModel)
	{
		//BindingContext = categoriesPageViewModel;
		BindingContext = searchPageViewModel;

        InitializeComponent();
	}

    private void CategoryOption_Tapped(object sender, EventArgs e)
    {
        //(BindingContext as CategoriesPageViewModel).CategoryOptionTappedGoBackToSearchPage.Execute(null);
        
        //(BindingContext as SearchPageViewModel).FilterByCategory(int.Parse((sender as Frame).ClassId));

        //var jsonStr = JsonConvert.SerializeObject((sender as Frame).ClassId);
        Shell.Current.GoToAsync($"../?categoryStr={(sender as Frame).ClassId}");
    }

  
}