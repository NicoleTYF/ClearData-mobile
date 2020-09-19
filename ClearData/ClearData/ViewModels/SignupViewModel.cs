using System;
using ClearData.Views;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using ClearData.Models;
using System.Net.Http.Headers;

namespace ClearData.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        public Command CreateAccCom { get; }

        private HttpClient client;
        private const string BaseURL = "https://cleardata-webapp.uqcloud.net/api/consumer_profiles/";

        private string uText;
        public string UsernameText
        {
            get => uText;
            set => SetProperty(ref uText, value);
        }

        private DateTime DOB;
        public DateTime DateofBirth
        {
            get => DOB;
            set => SetProperty(ref DOB, value);
        }

        private string place;
        public string Birthplace
        {
            get => place;
            set => SetProperty(ref place, value);
        }

        public SignupViewModel()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(BaseURL);
            CreateAccCom = new Command(createAccount);
            DateofBirth = DateTime.Now;
        }

        private async void createAccount()
        {
            if (Birthplace == null || UsernameText == null || DateofBirth == DateTime.Now)
            {
                if (Birthplace == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a birthplace", "OK");
                }
                else if (UsernameText == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a username", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter your date of birth", "OK");
                }
            }
            else
            {
                string basicUsername = "admin";
                string basicPWord = "BakedBeans3";
                var authdata = string.Format("{0}:{1}", basicUsername, basicPWord);
                var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authdata));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

                string dateofbirth = string.Format("{0}-{1:00}-{2:00}", DateofBirth.Year, DateofBirth.Month, DateofBirth.Day);
                Uri uri = new Uri(string.Format(BaseURL, string.Empty));
                var userInfo = new UserDataJson()
                {
                    username = UsernameText,
                    date_of_birth = dateofbirth,
                    birthplace = Birthplace
                };
                var jsonstring = JsonConvert.SerializeObject(userInfo);
                Console.WriteLine(jsonstring);
                var jsonContent = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, jsonContent);
                // Success + remember to set the static class elements
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    await Shell.Current.GoToAsync($"//AboutPage");
                }
                else
                {
                    var msg = string.Format("Request failed with error code {0}", response.StatusCode);
                    await Application.Current.MainPage.DisplayAlert("Alert", msg, "bummer");
                }
            }

        }
    }
}
