using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClearData.ViewModels
{
	public class LogHistoryViewModel : BaseViewModel
	{
        public Command BackBtnCommand { get; }

        public LogHistoryViewModel()
        {
            BackBtnCommand = new Command(OnBackBtnTapped);
        }

        private async void OnBackBtnTapped(object obj)
        {
            await Shell.Current.GoToAsync($"//LogsPage");
        }
    }
}