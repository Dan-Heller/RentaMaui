using Renta.Models;
using Renta.enums;
using Renta.Dto_s;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Renta.Services
{
    public class ReviewsService
    {
        UserService _userService;
        IConfiguration configuration;
        HttpClient httpclient;

        public ReviewsService(IConfiguration config, UserService userService)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };
            httpclient = new HttpClient(httpClientHandler);
            configuration = config;
            httpclient.MaxResponseContentBufferSize = 256000;
        }


        public async Task CreateItemReview(ItemReview itemReview)
        {

            string json = JsonConvert.SerializeObject(itemReview);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json" );


            await httpclient.PostAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Reviews/item/"), content);
        }


        public async Task CreateUserReview(UserReview userReview)
        {

            string json = JsonConvert.SerializeObject(userReview);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json" );


            await httpclient.PostAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Reviews/user/"), content);
        }






        public async Task<List<ItemReview>> GetReviewsOnItem(string itemId)
        {
            HttpResponseMessage response = null;

            
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Reviews/item/" + itemId));

            string str = await response.Content.ReadAsStringAsync();
            var itemReviews = JsonConvert.DeserializeObject<List<ItemReview>>(str);
            return itemReviews;
        }

        public async Task<List<UserReview>> GetReviewsOnUser(string userId)
        {

            HttpResponseMessage response = null;

           
            response = await httpclient.GetAsync(new Uri(configuration.GetSection("Settings:ApiUrl").Value + "/Reviews/user/" + userId));

            string str = await response.Content.ReadAsStringAsync();
            var userReviews = JsonConvert.DeserializeObject<List<UserReview>>(str);
            return userReviews;
        }



    }
}
