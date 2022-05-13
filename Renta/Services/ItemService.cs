using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Renta.Models;
using Renta.Dto_s;
using RestSharp;

namespace Renta.Services
{
    public class ItemService
    {
        //public User LoggedInUser = null;
        UserService _userService;
        IConfiguration configuration;
        HttpClient httpclient;
        // https://rentaapidev.azurewebsites.net
        public event Action UserUpdatedInvoker;


        public async Task UploadNewItem(Item item)
        {
            string json = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            //HttpResponseMessage response = null;

            
           await httpclient.PostAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items"), content);
           await _userService.UpdateLoggedInUser(); 

            //if (UserUpdatedInvoker != null)
            //{
            //    UserUpdatedInvoker.Invoke();
            //}

        }

        public async Task<List<Item>> GetItemsByOwner(string OwnerId)
        {

            HttpResponseMessage response = null;

            //string UserId = _userService.LoggedInUser.Id;
            response =  await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items/owner/" + OwnerId));
           
            string str = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Item>>(str);

            return items;

        }

        public async Task<List<Item>> GetLoggedInUserItems()
        {
            return  await GetItemsByOwner(_userService.LoggedInUser.Id);
        }


        public ItemService(IConfiguration config, UserService userService)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            httpclient = new HttpClient(httpClientHandler);
            configuration = config;
            httpclient.MaxResponseContentBufferSize = 256000;

            _userService = userService;
        }

        public async Task<List<Item>> GetItems()
        {
            HttpResponseMessage response = null;
            //string UserId = _userService.LoggedInUser.Id;
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items"));
            string str = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Item>>(str);
            return items;

        }

        public async Task<Item> GetItemById(string ItemId)
        {
            HttpResponseMessage response = null;
            //string UserId = _userService.LoggedInUser.Id;
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items/" + ItemId));
            string str = await response.Content.ReadAsStringAsync();
            var item = JsonConvert.DeserializeObject<Item>(str);
            return item;

        }

    }
}