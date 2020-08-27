using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ClearData.ViewModels
{
	public class LogsViewModel: BaseViewModel
	{
        public Command HistoryBtnCommand { get; }

        private ICommand _historyBtnCommand;

        public ICommand AddToCartCommand => _historyBtnCommand
            ?? (_historyBtnCommand = new Command<string>(OnHistoryBtnTapped));

        public List<ClearData.Models.DataType> Items { get; set; }

        public LogsViewModel ()
	    {
            Items = new ClearData.Services.PermissionsDataStore().dataTypes;

            // HistoryBtnCommand = new Command(OnHistoryBtnTapped);
        }

        private async void OnHistoryBtnTapped(string selectedItemId)
        {
            var item = Items.FirstOrDefault(x => x.Id.Equals(selectedItemId));

            if (item == null) {
                return;
            } else {
                await Shell.Current.GoToAsync($"//LogHistoryPage");
            }
        }
    }
}