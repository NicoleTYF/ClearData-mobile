using System;
using ClearData.Views;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

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

        private DateTime dob;
        public DateTime DOB
        {
            get => dob;
            set => SetProperty(ref dob, value);
        }

        private string place;
        public string Birthplace
        {
            get => place;
            set => SetProperty(ref place, value);
        }

        public SignupViewModel() 
        {
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
                // Success
                await Application.Current.MainPage.DisplayAlert("Alert", "Well done, you made it", "Hooray!");
            }
        }

    }
}
