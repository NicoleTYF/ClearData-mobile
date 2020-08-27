using System.ComponentModel;
using Xamarin.Forms;
using ClearData.ViewModels;
using System;

namespace ClearData.Views
{
    public partial class ManageCompanyPage : ContentPage
    {
        private ManageCompanyViewModel _viewModel;
        public ManageCompanyPage(ManageCompanyViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        public void Picker(object sender, System.EventArgs e)
        {
            Console.WriteLine("hello from the otherside");
            _viewModel.OnRestrictionChanged();
        }
    }
}