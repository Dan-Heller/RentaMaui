using Renta.ViewModels;
namespace Renta;

public partial class ItemPage : ContentPage
{
	public bool HeartButtonClicked { get; set; }

	public ItemPage(ItemPageViewModel itemPageViewModel)
	{
		BindingContext = itemPageViewModel;
		InitializeComponent();

		SetHeartButton();
		
	}

	private  void SetHeartButton()
    {
		if((BindingContext as ItemPageViewModel).ItemLiked == false)
        {
			HeartButton.IconImageSource = "hollowhearticon.png";
		}
        else
        {
			HeartButton.IconImageSource = "fullhearticon.png";
		}
    }

	protected override void OnAppearing()
	{
		base.OnAppearing();
		(BindingContext as ItemPageViewModel).deserializeString();

	}



	private void HeartButton_Clicked(object sender, EventArgs e)
    {
		(BindingContext as ItemPageViewModel).ItemLiked = !(BindingContext as ItemPageViewModel).ItemLiked;
		SetHeartButton();

	}


    private async void SendRequestTapped(object sender, EventArgs e)
    {
        (BindingContext as ItemPageViewModel).datesCollection = xCalendar.SelectedDates;
		await (BindingContext as ItemPageViewModel).sendItemRequest();
    }
}