using ClearData.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ClearData.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        public Command LoginCommand { get; }

        public Command SignupCommand { get; }

        private bool mVis = false;

        public string Username
        {
            get; set;
        }

        public string Password
        {
            get; set;
        }

        public bool MessageVisibility
        {
            get => mVis;
            set => SetProperty(ref mVis, value);
        }

        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            SignupCommand = new Command(OnSignupClicked);
        }

        private async void OnLoginClicked(object obj)
        {
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            if (Username == "1" && Password == "2")
            {
                await Shell.Current.GoToAsync($"//AboutPage");
            }
            else
            {
                MessageVisibility = true;
            }
        }

        private async void OnSignupClicked(object obj)
        {
            await Shell.Current.GoToAsync($"//SignupPage");
        }
    }
}
