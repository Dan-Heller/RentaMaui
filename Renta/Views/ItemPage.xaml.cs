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
		if(RequestAndDatesButton.Text == "Select dates")
        {
            xCalendar.IsVisible = !(xCalendar.IsVisible);
            RequestAndDatesButton.Text = "Send request";
			RequestAndDatesButton.BackgroundColor = Color.FromArgb("#008000");
        }
        else
        {
            if (xCalendar.SelectedDates.Count > 0)
            {
                (BindingContext as ItemPageViewModel).datesCollection = xCalendar.SelectedDates;
                await (BindingContext as ItemPageViewModel).sendItemRequest();
            }
            else
            {
                await DisplayAlert(" ", "Please Select desired dates.", "close");
            }
        }
    }
}