using Renta.ViewModels;
namespace Renta;

public partial class CategoriesPage : ContentPage
{
	public CategoriesPage(CategoriesPageViewModel categoriesPageViewModel)
	{
		BindingContext = categoriesPageViewModel;
		InitializeComponent();
	}

    private void CategoryOption_Tapped(object sender, EventArgs e)
    {
		(BindingContext as CategoriesPageViewModel).CategoryOptionTappedGoBackToSearchPage.Execute(null);
    }

  
}