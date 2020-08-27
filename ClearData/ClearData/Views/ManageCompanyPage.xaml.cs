using System.ComponentModel;
using Xamarin.Forms;
using ClearData.ViewModels;

namespace ClearData.Views
{
    public partial class ManageCompanyPage : ContentPage
    {
        public ManageCompanyPage(ManageCompanyViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}