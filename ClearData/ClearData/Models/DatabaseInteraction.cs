using ClearData.Services;
using ClearData.Views;
using System;
using System.Text;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace ClearData.Models
{
    

    public class DatabaseInteraction
    {

        private static string basicUsername = "admin";
        private static string basicPWord = "BakedBeans3";
        private static string webpage = "https://cleardata-webapp.uqcloud.net/api";
        private static HttpClient client;

        public enum DatabaseRequest { SIGNUP, DATATYPES, ENTERPRISES, WANTED_DATA_TYPES, USER_PERMISSIONS, USER_LOGS }
        public enum HttpRequestType { POST, GET }

        private static Uri GetUri(DatabaseRequest requestType)
        {
            String address;
            switch(requestType)
            {
                case DatabaseRequest.SIGNUP:
                    address = "consumer_profiles";
                    break;
                case DatabaseRequest.ENTERPRISES:
                    address = "enterprises";
                    break;
                case DatabaseRequest.WANTED_DATA_TYPES:
                    address = "enterprise_data_types";
                    break;
                case DatabaseRequest.USER_LOGS:
                    address = "user_logs";
                    break;
                case DatabaseRequest.USER_PERMISSIONS:
                    address = "user_permissions";
                    break;
                case DatabaseRequest.DATATYPES:
                default: //never uses default
                    address = "data_types";
                    break;
            }
            //construct the URI string
            String uriString = String.Format("{0}/{1}/", webpage, address);
            //then return a URI with this string, not sure this format actually does anything
            return new Uri(uriString);
        }

        public static async Task<HttpResponseMessage> SendDatabaseRequest(DatabaseRequest requestType, HttpRequestType type, HttpContent httpContent)
        {
            client = new HttpClient();
            client.BaseAddress = GetUri(requestType);
            var authdata = string.Format("{0}:{1}", basicUsername, basicPWord);
            var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authdata));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            //then send the request
            HttpResponseMessage response;
            switch (type)
            {
                case HttpRequestType.POST:
                    response = await client.PostAsync(GetUri(requestType), httpContent);
                    break;
                case HttpRequestType.GET:
                default:
                    response = await client.GetAsync(GetUri(requestType));
                    break;

            }

            if (!response.IsSuccessStatusCode)
            {
                var msg = string.Format("Request failed with error code {0}", response.StatusCode);
                await Application.Current.MainPage.DisplayAlert("Alert", msg, "bummer");
            }
            return response;
        }
    }
}