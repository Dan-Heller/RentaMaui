using Microsoft.AspNetCore.SignalR.Client;

namespace Renta;

public partial class MessagesPage : ContentPage
{
	private readonly HubConnection _connection;

	public MessagesPage()
	{
		InitializeComponent();

		_connection = new HubConnectionBuilder().WithUrl("https://474b-85-65-247-184.eu.ngrok.io/chat").Build();
		_connection.On<string>("MessageReceived", (message) =>
		{
			chatMessages.Text += $"{Environment.NewLine}{message}";
		} );

		Task.Run(() =>
		{
			Dispatcher.Dispatch(async () =>
			await _connection.StartAsync());
		});

    }

	private async void SendButton_Clicked(object sender, EventArgs e)
	{
		await _connection.InvokeCoreAsync("SendMessage", args: new[] { myChatMessage.Text });

		myChatMessage.Text = string.Empty;
	}
}