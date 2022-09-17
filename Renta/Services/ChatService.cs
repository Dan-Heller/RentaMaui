using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Renta.Dto_s;
using Renta.Models;
using Renta.ViewModels;
using System.Text;



namespace Renta.Services
{
    public class ChatService
    {
        private readonly HubConnection _connection;
        UserService _userService;
        IConfiguration configuration;
        HttpClient httpclient;
        public string userId;
        public MessagesPageViewModel currentMessagesPage;

        public ChatService(IConfiguration config, UserService userService)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            httpclient = new HttpClient(httpClientHandler);
            configuration = config;
            httpclient.MaxResponseContentBufferSize = 256000;
            _userService = userService;
            userId = userService.LoggedInUser.Id;

            //create the signalRT group for the user 
            _connection = new HubConnectionBuilder().WithUrl(configuration.GetSection("Settings:ApiUrl").Value + "/chat?userid=" + userId).Build();

            _connection.On<string, string>("ReceiveMessage", (sender, message) =>
            {
                Message newMessage = new Message(sender, message, DateTime.Now);
                currentMessagesPage.AddMessageToCollection(newMessage);

            });

            Task.Run(async () =>
            {
                await _connection.StartAsync();
            });

        }

        public async Task InvokeSend(string message)
        {
            MessageDto messageDto = new MessageDto(userId, currentMessagesPage._currentChatViewModel.OtherUserId, message, currentMessagesPage._currentChat.Id, DateTime.Now);
            await _connection.InvokeCoreAsync("SendMessageToGroup", args: new[] { messageDto });

            Message newMessage = new Message(_userService.LoggedInUser.Id, message, DateTime.Now);
            currentMessagesPage.AddMessageToCollection(newMessage);
        }

        public async Task<Chat> CreateNewChat(CreateChatDto createChatDto)
        {
            string json = JsonConvert.SerializeObject(createChatDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await httpclient.PostAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Users/Chat/"), content);
            string str = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Chat>(str);
        }
    }
}
