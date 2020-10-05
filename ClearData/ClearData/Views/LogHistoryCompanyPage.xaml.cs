using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearData.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace ClearData.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LogHistoryCompanyPage : ContentPage
	{
        public ObservableCollection<string> Items { get; set; }
        private LogHistoryCompanyViewModel _viewModel;

        public LogHistoryCompanyPage(LogHistoryCompanyViewModel viewModel)  
		{
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            LogHistory_Today.HeightRequest = _viewModel.TodaysLogs.Count * LogHistory_Today.RowHeight + 10;
            LogHistory_ThisWeek.HeightRequest = _viewModel.ThisWeeksLogs.Count * LogHistory_ThisWeek.RowHeight + 10;
            LogHistory_Past.HeightRequest = _viewModel.LaterLogs.Count * LogHistory_Past.RowHeight + 10;
        }

        public void expandTodayList(object sender, System.EventArgs e)
        {
            if (LogHistory_Today.IsVisible == true)
            {
                LogHistory_Today.IsVisible = false;
                todayArrowImg.Source = "arrow_down_wt.png";
            }
            else
            {
                LogHistory_Today.IsVisible = true;
                todayArrowImg.Source = "arrow_up_wt.png";
            }
        }

        public void expandWeekList(object sender, System.EventArgs e)
        {
            if (LogHistory_ThisWeek.IsVisible == true)
            {
                LogHistory_ThisWeek.IsVisible = false;
                weekArrowImg.Source = "arrow_down_wt.png";
            }
            else
            {
                LogHistory_ThisWeek.IsVisible = true;
                weekArrowImg.Source = "arrow_up_wt.png";
            }
        }

        public void expandPastList(object sender, System.EventArgs e)
        {
            if (LogHistory_Past.IsVisible == true)
            {
                LogHistory_Past.IsVisible = false;
                pastArrowImg.Source = "arrow_down_wt.png";
            }
            else
            {
                LogHistory_Past.IsVisible = true;
                pastArrowImg.Source = "arrow_up_wt.png";
            }
        }

        void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e == null)
            {
                return;
            }
            else
            {
                // de-select the row 
                ((ListView)sender).SelectedItem = null;
            }
        }
    }
}