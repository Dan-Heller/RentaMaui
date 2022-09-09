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
using Autofac.Features.OwnedInstances;

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

            //if (UserUpdatedInvoker != null )
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

        public async Task<List<Item>> GetLoggedInUserLikedItems()
        {
            HttpResponseMessage response = null;

            string UserId = _userService.LoggedInUser.Id;
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Users/ItemsFromIds/" + UserId));

            string str = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Item>>(str);

            return items;
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

        public async Task<List<Item>> GetOtherUsersItems()
        {
            HttpResponseMessage response = null;
            string UserId = _userService.LoggedInUser.Id;
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items/search-all/"+ UserId));
            string str = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Item>>(str);
            return items;

        }

        public async Task<List<Item>> GetItemsNearYouByRegion()
        {
            HttpResponseMessage response = null;
            string Id = _userService.LoggedInUser.Id;
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items/near-you/" + Id));
            string str = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Item>>(str);
            return items;

        }

        public async Task<List<Item>> GetItemsByFavoritesCategories()
        {
            HttpResponseMessage response = null;
            string Id = _userService.LoggedInUser.Id;
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items/might-like/" + Id));
            string str = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<List<Item>>(str);
            return items;

        }

        public async Task<List<Item>> GetItemsByText(string text)
        {
            HttpResponseMessage response = null;
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items/search/" + text));
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

        public async Task UpdateTransaction(Item updatedItem)
        {
            string json = JsonConvert.SerializeObject(updatedItem);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;


            response = await httpclient.PutAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items/" + updatedItem.Id), content);

            string str = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Item>(str);
        }


        public async Task UpdateItemInfo(Item item)
        {
            string json = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;

            string ItemId = item.Id;
            response = await httpclient.PutAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Items/" + ItemId), content);

            //string str = await response.Content.ReadAsStringAsync();

            //need to make sure that after save button pressed the page will get item again . 

            //LoggedInUser = JsonConvert.DeserializeObject<User>(str);

            //if (UserUpdatedInvoker != null)
            //{
            //    UserUpdatedInvoker.Invoke();
            //}

        }

    }
}