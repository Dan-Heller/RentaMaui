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
            _connection = new HubConnectionBuilder().WithUrl("https://ef57-85-65-247-184.eu.ngrok.io/chat?userid=" + userId).Build();

            //_connection.On<string>("MessageReceived", (message) =>
            //{
            //    chatMessages.Text += $"{Environment.NewLine}{message}";
            //});

            _connection.On<string, string>("ReceiveMessage", (sender, message) =>
            {
                Message newMessage = new Message(sender, message, DateTime.Now);
                currentMessagesPage.AddMessageToCollection(newMessage);

                //chatMessages.Text += $"{Environment.NewLine}{message}";
                //chatMessages.Text += $"{Environment.NewLine}{sender}";
            });

            Task.Run(async () =>
            {
               //Dispatcher.Dispatch(async () =>
               // await _connection.StartAsync());

                await _connection.StartAsync();
            });

        }

        public async Task InvokeSend(string message)
        {
            //string sender, string receiver, string message, string chatId, DateTime createdAt
            MessageDto messageDto = new MessageDto(userId, currentMessagesPage._currentChat.OtherUserId, message, currentMessagesPage._currentChat._chat.Id, DateTime.Now);
            await _connection.InvokeCoreAsync("SendMessageToGroup", args: new[] { messageDto });
        }

        public async Task CreateNewChat(CreateChatDto createChatDto)
        {
            string json = JsonConvert.SerializeObject(createChatDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await httpclient.PostAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Users/Chat/"), content);
           
        }
    }
}
