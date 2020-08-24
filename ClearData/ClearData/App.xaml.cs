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

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            Console.WriteLine("Baked beans is how we'll start!");

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
