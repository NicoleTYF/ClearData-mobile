using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ClearData.ViewModels;

namespace ClearData.Views
{
    public partial class ManageDataPage : ContentPage
    {

        ManageDataViewModel _viewModel;

        public ManageDataPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new ManageDataViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void OnServicePushed(object sender, EventArgs e)
        {
            _viewModel.UpdateToServicesDisplay();
        }

        private void OnDataTypesPushed(object sender, EventArgs e)
        {
            _viewModel.UpdateToDataTypesDisplay();
        }
    }
}