namespace Renta.Controls;

public partial class RentingPageTabsControl : ContentView
{


	public RentingPageTabsControl()
	{
		InitializeComponent();
		BindingContext = this;
	}

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
		RequestsLabel.TextColor = Color.FromHex("808080");
		ApprovedLabel.TextColor = Color.FromHex("808080");
		ActiveLabel.TextColor = Color.FromHex("808080");
		HistoryLabel.TextColor = Color.FromHex("808080");

		(sender as Label).TextColor = Color.FromHex("000000");
	}
}