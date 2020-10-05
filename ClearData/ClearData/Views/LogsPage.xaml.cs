using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClearData.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ClearData
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogsPage : ContentPage
    {
        LogsViewModel _viewModel;
        public LogsPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new LogsViewModel();
            DataTypeLogList.HeightRequest = 4.3 * CompanyLogList.RowHeight;
            CompanyLogList.HeightRequest = 4.3 * CompanyLogList.RowHeight;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void OpenInfo(object sender, System.EventArgs e)
        {
            DisplayAlert("How to use", "Click on a data type or service provider to view full log history.", "Got it");
        }

        private async void OnDataTypesPushed(object sender, System.EventArgs e)
        {
            await _viewModel.UpdateToDataTypesDisplay();
        }

        private async void OnServicePushed(object sender, System.EventArgs e)
        {
            await _viewModel.UpdateToServicesDisplay();
        }
    }
}
