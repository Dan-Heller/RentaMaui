
namespace Renta.Controls;

public partial class RentingTransactionControl : ContentView
{
    public RentingTransactionControl()
    {
        InitializeComponent();
    }

    private async void ItemImage_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(MyItemPage)}");
    }

    private async void OtherUserProfile_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(OtherUserProfilePage)}");
    }
}