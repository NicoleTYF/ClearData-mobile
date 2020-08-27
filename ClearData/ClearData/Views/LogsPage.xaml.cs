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
        public LogsPage()
        {
            InitializeComponent();
            LogsViewModel logsViewModel = new LogsViewModel();
			this.BindingContext = logsViewModel;
			LogList.ItemsSource = logsViewModel.Items;
        }
    }
}
