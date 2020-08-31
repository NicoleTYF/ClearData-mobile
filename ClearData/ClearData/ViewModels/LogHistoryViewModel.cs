using ClearData.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ClearData.ViewModels
{
	public class LogHistoryViewModel : BaseViewModel
	{
        public DataType DataType { get; set; }
        public ObservableCollection<Log> TodaysLogs { get; }
        public ObservableCollection<Log> ThisWeeksLogs { get; }
        public ObservableCollection<Log> LaterLogs { get; }

        public Command BackButtonPressed { get; }

        public LogHistoryViewModel(DataType dataType, ObservableCollection<Log> allLogs)
        {
            Title = "Log History";
            BackButtonPressed = new Command(async () => await ExecuteBackButtonPressed());
            DataType = dataType;
            TodaysLogs = new ObservableCollection<Log>();
            ThisWeeksLogs = new ObservableCollection<Log>();
            LaterLogs = new ObservableCollection<Log>();
            LoadLogs(allLogs);
        }

        private void LoadLogs(ObservableCollection<Log> allLogs)
        {
            //clear all of the logs
            TodaysLogs.Clear();
            ThisWeeksLogs.Clear();
            LaterLogs.Clear();
            DateTime currentTime = DateTime.Now;
            foreach (Log log in allLogs)
            {
                //work out how long it was ago
                int daysPassed = (currentTime.Date - log.Time.Date).Days;
                if (daysPassed <= 0)
                    TodaysLogs.Add(log);
                else if (daysPassed < 7)
                    ThisWeeksLogs.Add(log);
                else
                    LaterLogs.Add(log);
            }
        }

        async Task ExecuteBackButtonPressed()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
    }
}