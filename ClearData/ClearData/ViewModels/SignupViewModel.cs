using System;
using ClearData.Views;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Net.Http;
using Xamarin.Forms;
using Newtonsoft.Json;
using ClearData.Models;

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
        }

        private async void createAccount()
        {
            if (Birthplace == null || UsernameText == null || DOB.Year == 1900)
            {
                // Fail
                await Application.Current.MainPage.DisplayAlert("Alert", "BEEEEEEAAAANSSSSS", "Okay..?");
            }
            else
            {
                Uri uri = new Uri(string.Format(BaseURL, string.Empty));
                var userInfo = new UserDataJson()
                {
                    username = UsernameText,
                    dob = DateofBirth,
                    birthplace = Birthplace
                };
                var jsonstring = JsonConvert.SerializeObject(userInfo);
                var jsonContent = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                var response = await client.PostAsync(uri, jsonContent);
                // Success + remember to set the static class elements
                await Application.Current.MainPage.DisplayAlert("Alert", "Well done, you made it", "Hooray!");
            }
        }

    }
}
