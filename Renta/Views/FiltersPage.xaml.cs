using Renta.Services;
using Renta.ViewModels;

namespace Renta;

public partial class FiltersPage : ContentPage
{
	SearchPageStateService _searchPageStateService;

	public FiltersPage(SearchPageStateService searchPageStateService, FilterPageViewModel filterPageViewModel)
	{
        _searchPageStateService = searchPageStateService;
		BindingContext  = filterPageViewModel;
        InitializeComponent();
	}

	private void Show_Clicked(object sender, EventArgs e)
	{
		_searchPageStateService.PriceRangeStart = (int)PriceSlider.RangeStart;
        _searchPageStateService.PriceRangeEnd = (int)PriceSlider.RangeEnd;

		if(RegionPicker.SelectedItem != null)
		{
            _searchPageStateService.SelectedRegion = RegionPicker.SelectedItem.ToString();
        }
		else
		{
			_searchPageStateService.SelectedRegion = "All";
        }
		

        Shell.Current.GoToAsync("..");
    }
}