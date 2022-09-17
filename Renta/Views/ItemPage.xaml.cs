using Renta.ViewModels;

namespace Renta;

public partial class ItemPage : ContentPage
{
    public bool HeartButtonClicked { get; set; }

    public ItemPage(ItemPageViewModel itemPageViewModel)
    {
        BindingContext = itemPageViewModel;

        InitializeComponent();
    }

    private void SetHeartButton()
    {
        if ((BindingContext as ItemPageViewModel).ItemLiked == false)
        {
            HeartButton.IconImageSource = "hollowhearticon.png";
        }
        else
        {
            HeartButton.IconImageSource = "fullhearticon.png";
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        (BindingContext as ItemPageViewModel).deserializeString();
        await (BindingContext as ItemPageViewModel).FetchItemReviews();
        SetHeartButton();
    }

    private void HeartButton_Clicked(object sender, EventArgs e)
    {
        (BindingContext as ItemPageViewModel).ItemLiked = !(BindingContext as ItemPageViewModel).ItemLiked;
        SetHeartButton();
    }


    private async void SendRequestTapped(object sender, EventArgs e)
    {
        if (RequestAndDatesButton.Text == "Select dates")
        {
            MainCalendarView.IsVisible = !(MainCalendarView.IsVisible);
            RequestAndDatesButton.Text = "Send request";
            RequestAndDatesButton.BackgroundColor = Color.FromArgb("#008000");
        }
        else
        {
            if ((BindingContext as ItemPageViewModel).EventCalendar.SelectedDates.Count > 0)
            {
                await (BindingContext as ItemPageViewModel).sendItemRequest();
            }
            else
            {
                await DisplayAlert(" ", "Please Select desired dates.", "close");
            }
        }
    }
}