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
        Items.TextColor = Color.FromHex("808080");
        Reviews.TextColor = Color.FromHex("808080");
        (sender as Label).TextColor = Color.FromHex("000000");
    }
}