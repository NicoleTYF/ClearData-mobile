using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ClearData.ViewModels
{
    public class ManageDataViewModel : BaseViewModel
    {
        public ManageDataViewModel()
        {
            Title = "Manage Data";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamain-quickstart"));
        }

        public ICommand OpenWebCommand { get; }
    }
}