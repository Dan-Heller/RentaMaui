namespace Renta.Controls;

public partial class OtherUserItemsControl : ContentView
{


	public OtherUserItemsControl()
	{
		InitializeComponent();
		BindingContext = this;
	}

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
		AllLabel.TextColor = Color.FromHex("808080");
		UnAvailableLabel.TextColor = Color.FromHex("808080");
		AvailableLabel.TextColor = Color.FromHex("808080");

		(sender as Label).TextColor = Color.FromHex("000000");
	}
}