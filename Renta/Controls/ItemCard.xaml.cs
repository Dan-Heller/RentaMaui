namespace Renta.Controls;

public partial class ItemCard : ContentView
{
    public bool HeartButtonClicked { get; set; }

    public ItemCard()
    {
        InitializeComponent();
        HeartButtonClicked = false;
    }

    private async void ItemCard_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ItemPage)}");
    }

    private void onCardHeartTapped(object sender, EventArgs e)
    {
        if (HeartButtonClicked == false)
        {
            HeartButton.Source = "fullhearticon.png";
        }
        else
        {
            HeartButton.Source = "hollowhearticon.png";
        }

        HeartButtonClicked = !HeartButtonClicked;
    }
}