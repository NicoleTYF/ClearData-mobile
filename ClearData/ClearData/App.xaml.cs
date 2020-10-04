using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClearData.Services;
using ClearData.Views;

namespace ClearData
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override async void OnStart()
        {
            Console.WriteLine("Baked beans is how we'll start!");
            await Shell.Current.GoToAsync("//LoginPage");
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
