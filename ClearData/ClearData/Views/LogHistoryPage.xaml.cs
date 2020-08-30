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
	public partial class LogHistoryPage : ContentPage
	{
        public ObservableCollection<string> Items { get; set; }
        private LogHistoryViewModel _viewModel;

        public LogHistoryPage(LogHistoryViewModel viewModel)  
		{
            InitializeComponent();
            BindingContext = _viewModel = viewModel;

        }
    }
}