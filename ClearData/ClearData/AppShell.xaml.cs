using System;
using System.Collections.Generic;
using ClearData.ViewModels;
using ClearData.Views;
using Xamarin.Forms;

namespace ClearData
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}