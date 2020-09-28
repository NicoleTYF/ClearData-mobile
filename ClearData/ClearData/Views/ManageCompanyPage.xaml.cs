using System.ComponentModel;
using Xamarin.Forms;
using ClearData.ViewModels;
using System;

namespace ClearData.Views
{
    public partial class ManageCompanyPage : ContentPage
    {
        private bool isExpand = false;

        private ManageCompanyViewModel _viewModel;
        public ManageCompanyPage(ManageCompanyViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
        }

        public void expandCompanyInfo(object sender, System.EventArgs e)
        {
            isExpand = !isExpand;

            if(isExpand == true) {
                companyInfo.LineBreakMode = LineBreakMode.WordWrap;
                expandBtn.Source = "arrow_up_yl.png";
            } else {
                companyInfo.LineBreakMode = LineBreakMode.TailTruncation;
                expandBtn.Source = "arrow_down_yl.png";
            }
        }

        public void Picker(object sender, System.EventArgs e)
        {
            _viewModel.OnRestrictionChanged();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void SwitchToggled(object sender, ToggledEventArgs e)
        {
            _viewModel.SwitchToggled();
        }
    }
}