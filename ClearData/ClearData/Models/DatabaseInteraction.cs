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
    /**
     * class for interaction with the database, allows put, post and get requests and handles all the endpoint locations
     * the SendDatabaseRequest is the bulk of this class and the interesting part
     */
    public class DatabaseInteraction
    {
        private static string webpage = "https://cleardata-webapp.uqcloud.net/api";
        private static HttpClient client;

        public enum DatabaseRequest { SIGNUP, DATATYPES, ENTERPRISES, WANTED_DATA_TYPES, USER_PERMISSIONS, ALLOW_PERMISSION, DENY_PERMISSION, USER_LOGS, LOGIN }
        public enum HttpRequestType { POST, GET }

        private static Uri GetUri(DatabaseRequest requestType)
        {
            String address;
            switch(requestType)
            {
                case DatabaseRequest.SIGNUP:
                    address = "new_consumer";
                    break;
                case DatabaseRequest.ENTERPRISES:
                    address = "enterprises";
                    break;
                case DatabaseRequest.WANTED_DATA_TYPES:
                    address = "enterprise_data_types";
                    break;
                case DatabaseRequest.USER_LOGS:
                    address = "data_access";
                    break;
                case DatabaseRequest.USER_PERMISSIONS:
                    address = "user_permissions";
                    break;
                case DatabaseRequest.ALLOW_PERMISSION:
                    address = "allow_permission";
                    break;
                case DatabaseRequest.DENY_PERMISSION:
                    address = "deny_permission";
                    break;
                case DatabaseRequest.LOGIN:
                    address = "authenticate";
                    break;
                case DatabaseRequest.DATATYPES:
                default: //never uses default, but make the compiler happy
                    address = "data_types";
                    break;
            }
            //construct the URI string
            String uriString = String.Format("{0}/{1}/", webpage, address);
            //then return a URI with this string, not sure this format actually does anything
            return new Uri(uriString);
        }

        public static async Task<HttpResponseMessage> SendDatabaseRequest(DatabaseRequest requestType, HttpRequestType type, HttpContent httpContent, Boolean auth, Boolean displayErrors)
        {
            client = new HttpClient();
            client.BaseAddress = GetUri(requestType);

            //add authentication to the request, this won't be done for a login/signup request, but is done for any other request
            if (auth)
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", UserInfo.DatabaseInfo.token);
            }

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

            if (!response.IsSuccessStatusCode && displayErrors)
            {
                var msg = string.Format("Request failed with error code {0}", response.StatusCode);
                await Application.Current.MainPage.DisplayAlert("Alert", msg, "continue");
            }
            return response;
        }
    }
}