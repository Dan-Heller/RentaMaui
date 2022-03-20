namespace Renta.Controls;

public partial class ItemCard : ContentView
{


	public ItemCard()
	{
		InitializeComponent();
	}

    private async void ItemCard_Tapped(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"{nameof(ItemPage)}");
	}
}