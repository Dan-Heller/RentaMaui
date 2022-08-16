namespace Renta.Controls;

public partial class ListChatControl : ContentView
{
	public ListChatControl()
	{
		InitializeComponent();
	}

	private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
	{
        await Shell.Current.GoToAsync($"{nameof(MessagesPage)}");
    }
}