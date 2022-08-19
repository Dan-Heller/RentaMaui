using AndroidX.Lifecycle;
using Microsoft.AspNetCore.SignalR.Client;
using Renta.Services;
using Renta.ViewModels;

namespace Renta;

public partial class MessagesPage : ContentPage
{
	//private readonly HubConnection _connection;
	private UserService _userService;
	private ChatService _chatService;
	public string userId; 

    public MessagesPage(UserService userService, ChatService chatService, MessagesPageViewModel messagesPageViewModel)
	{
		BindingContext = messagesPageViewModel;
		_chatService = chatService;
		_userService = userService;
        userId = _userService.LoggedInUser.Id;
		Resources.Add("loggedInUserId", userId);
        InitializeComponent();

		//_connection = new HubConnectionBuilder().WithUrl("https://ef57-85-65-247-184.eu.ngrok.io/chat?userid=" + userId).Build();

		//_connection.On<string>("MessageReceived", (message) =>
		//{
		//	chatMessages.Text += $"{Environment.NewLine}{message}";
		//} );

		//      _connection.On<string,string>("ReceiveMessage", (sender,message) =>
		//      {
		//          chatMessages.Text += $"{Environment.NewLine}{message}";
		//          chatMessages.Text += $"{Environment.NewLine}{sender}";
		//      });

		//Task.Run(() =>
		//{
		//	Dispatcher.Dispatch(async () =>
		//	await _connection.StartAsync());
		//});


		//Task.Run(async () =>
		//{
		//	await _connection.InvokeCoreAsync("OnConnectedAsync", args: new[] { "text" });
		//});

	}

    //private async void SendButton_Clicked(object sender, EventArgs e)
    //{
    //	await _connection.InvokeCoreAsync("SendMessage", args: new[] { myChatMessage.Text });

    //	myChatMessage.Text = string.Empty;
    //}

    protected override  void OnAppearing()
    {
		base.OnAppearing();
		(BindingContext as MessagesPageViewModel).FetchMessagesFromChat();

    }

    private async void SendButton_Clicked(object sender, EventArgs e)
    {
		//await _connection.InvokeCoreAsync("SendMessageToGroup", args: new[] { userId, "62f6353590526b2d900f2e99", myChatMessage.Text });

		await _chatService.InvokeSend(myChatMessage.Text);

        myChatMessage.Text = string.Empty;
    }
}