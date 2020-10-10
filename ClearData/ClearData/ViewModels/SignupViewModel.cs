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

        private string pword1;
        public string Password
        {
            get => pword1;
            set => SetProperty(ref pword1, value);
        }

        private string pword2;
        public string PasswordCheck
        {
            get => pword2;
            set => SetProperty(ref pword2, value);
        }

        public SignupViewModel()
        {
            CreateAccCom = new Command(createAccount);
            DateofBirth = DateTime.Now;
        }

        private async void createAccount()
        {
            if (Birthplace == null || UsernameText == null || DateofBirth == DateTime.Now || 
                Password == null || PasswordCheck == null || !(Password.Equals(PasswordCheck)))
            {
                if (Birthplace == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a birthplace", "OK");
                }
                else if (UsernameText == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a username", "OK");
                }
                else if (Password == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a password", "OK");
                }
                else if (PasswordCheck == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please confirm your password", "OK");
                }
                else if (DateofBirth == DateTime.Now)
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Please enter your date of birth", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Passwords do not match", "OK");
                }
            }
            else
            {
                string dateofbirth = string.Format("{0}-{1:00}-{2:00}", DateofBirth.Year, DateofBirth.Month, DateofBirth.Day);
                var userInfo = new UserDataJson()
                {
                    auth = new Auth()
                    {
                        username = UsernameText,
                        password = Password
                    },
                    profile = new Profile()
                    {
                        birthplace = Birthplace,
                        date_of_birth = dateofbirth
                    }
                };
                var jsonstring = JsonConvert.SerializeObject(userInfo);
                Console.WriteLine(jsonstring);
                var jsonContent = new StringContent(jsonstring, Encoding.UTF8, "application/json");
                var response = await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.SIGNUP, 
                            DatabaseInteraction.HttpRequestType.POST, jsonContent, false, true);
                // Success + remember to set the static class elements
                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    UserInfo.DatabaseInfo = JsonConvert.DeserializeObject<DatabaseInfo>(jsonString);

                    await UserInfo.LoadPermissionsDataStore(); //added this here to initialise the whole permissions structure
                    await Shell.Current.GoToAsync($"//AboutPage");
                }
            }
        }
    }
}
