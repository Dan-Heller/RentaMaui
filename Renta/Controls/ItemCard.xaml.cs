namespace Renta.Controls;

public partial class ItemCard : ContentView
{

	public bool HeartButtonClicked { get; set; }

	public ItemCard()
	{
		InitializeComponent();
	}

    private async void ItemCard_Tapped(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"{nameof(ItemPage)}");
	}

    private void onCardHeartTapped(object sender, EventArgs e)
    {
		if(HeartButtonClicked == false)
        {
			HeartButton.Source = "hollowhearticon.png";
		}
        else
        {
			HeartButton.Source = "fullhearticon.png";
		}
		HeartButtonClicked = !HeartButtonClicked;
	}
}