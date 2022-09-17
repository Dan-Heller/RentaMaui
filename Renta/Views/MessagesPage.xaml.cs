using Renta.Services;
using Renta.ViewModels;

namespace Renta;

public partial class MessagesPage : ContentPage
{
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
    }


    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await (BindingContext as MessagesPageViewModel).FetchMessagesFromChat();
        ChatHeader.Text = "Chat with " +
                          (BindingContext as MessagesPageViewModel)._currentChatViewModel._otherUser.GetFullName();
    }

    private async void SendButton_Clicked(object sender, EventArgs e)
    {
        await _chatService.InvokeSend(myChatMessage.Text);

        myChatMessage.Text = string.Empty;
    }
}