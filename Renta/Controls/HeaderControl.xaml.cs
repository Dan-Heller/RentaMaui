namespace Renta.Controls;

public partial class HeaderControl : ContentView
{
    public static readonly BindableProperty ShowProfileButtonProperty =
        BindableProperty.Create(
            nameof(ShowProfileButtonProperty),
            typeof(bool),
            typeof(HeaderControl),
            true);

    public bool ShowProfileButton
    {
        get { return (bool)GetValue(ShowProfileButtonProperty); }
        set { SetValue(ShowProfileButtonProperty, value); }
    }

    public HeaderControl()
    {
        InitializeComponent();
    }

    private async void ProfileButton_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(ProfilePage)}");
    }

    private async void AddItemButton_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(AddItemPage)}");
    }

    private async void NotificationsButton_Tapped(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(NotificationsPage)}");
    }
}