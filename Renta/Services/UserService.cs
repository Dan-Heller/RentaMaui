using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Renta.Models;
using Renta.Dto_s;
using RestSharp;

namespace Renta.Services
{
    public class UserService
    {

       private readonly RestClient _client = new RestClient("https://rentaapidev.azurewebsites.net");
        //private readonly RestClient _client = new RestClient("http://10.0.2.2:3000");

        HttpClient httpclient;


        public UserService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };


            httpclient = new HttpClient(httpClientHandler);


            //httpclient.BaseAddress = new Uri("http://10.0.2.2:3000/omer/thruth"); //andoid local working!
            //httpclient.BaseAddress = new Uri("https://10.0.2.2:3000/Auth/register");
            //httpclient.BaseAddress = new Uri("https://e006-89-138-198-193.ngrok.io:3000/omer/thruth");
            httpclient.MaxResponseContentBufferSize = 256000;

        }

        public async Task RegisterUser(RegisterDto registerDto)
        {
            //var request = new RestRequest("​/Auth​/register", Method.Post).AddJsonBody(registerDto);
            //var response = await _client.PostAsync<string>(request);

            string json = JsonConvert.SerializeObject(registerDto);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = null;
            response = await httpclient.PostAsync(new Uri("https://rentaapidev.azurewebsites.net/Auth/register"), content);
            
        }



        //public async Task<User> LoginUser(Email email)
        //{
        //    string json = JsonConvert.SerializeObject(email);
        //    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = null;

        //    string str = string.Empty;
        //    response = await httpclient.PostAsync(new Uri("http://10.0.2.2:3000/Auth/login"), content);
        //    str = await response.Content.ReadAsStringAsync();
        //    User loggedUser = JsonConvert.DeserializeObject<User>(str);


        //    User user = new User(loggedUser.Username, loggedUser.CellphoneNumber, loggedUser.Password, loggedUser.Email);
        //    return user;
        //}


    }
}
