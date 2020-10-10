using System.ComponentModel;
using Xamarin.Forms;
using ClearData.ViewModels;
using System;
using System.Windows.Input;
using System.Collections.Generic;
using ClearData.Models;

namespace ClearData.Views
{
    public partial class ManageCompanyPage : ContentPage
    {
        public bool isExpand;
        public String permLevelTitle;
        public String permLevelDesc;
        public String introText;

        private ManageCompanyViewModel _viewModel;
        public ManageCompanyPage(ManageCompanyViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = _viewModel = viewModel;
            isExpand = false;
            companyInfo.LineBreakMode = LineBreakMode.TailTruncation;
            companyInfo.MaxLines = 3;
            setPermImage();
            setPermText();
        }

        public void Picker(object sender, System.EventArgs e)
        {
            _viewModel.OnRestrictionChangedAsync();
            setPermImage();
            setPermText();
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

        private void OpenInfo(object sender, System.EventArgs e)
        {
            DisplayAlert(permLevelTitle , permLevelDesc, "Close");
        }

        private void setPermImage() {
            if (_viewModel.CurrentRestriction == (int)Company.RestrictionType.ALL)
            {
                permImage.Source = "btn_full_access.png";
            } else if (_viewModel.CurrentRestriction == (int)Company.RestrictionType.CUSTOM)
            {
                permImage.Source = "btn_custom.png";

            } else if (_viewModel.CurrentRestriction == (int)Company.RestrictionType.NONE) 
            {
                permImage.Source = "btn_no_access.png";
            }
        }

        private void setPermText()
        {
            permLevelDesc = "Data level shows the percentage of company's customer data you opt to share with. \n\n";

            if (_viewModel.CurrentRestriction == (int)Company.RestrictionType.ALL)
            {
                permLevelTitle = "All";
                permLevelDesc += "On this setting, any data you are sharing will be accessible by this company";
            } 
            else if(_viewModel.CurrentRestriction == (int)Company.RestrictionType.CUSTOM)
            {
                permLevelTitle = "Custom";
                permLevelDesc += "On this setting, you can toggle access to any data types";

                //permPercent.Text = "Auto";
            } 
            else if (_viewModel.CurrentRestriction == (int)Company.RestrictionType.NONE)
            {
                permLevelTitle = "None";
                permLevelDesc += "On this setting, none of your data will be shared with this company";
            }
            
        }

        public void expandCompanyInfo(object sender, System.EventArgs e)
        {

            isExpand = !isExpand;

            if (isExpand == true)
            {
                companyInfo.LineBreakMode = LineBreakMode.WordWrap;
                companyInfo.MaxLines = int.MaxValue;
                expandBtn.Source = "arrow_up_yl.png";
            }
            else
            {
                companyInfo.LineBreakMode = LineBreakMode.TailTruncation;
                companyInfo.MaxLines = 3;
                expandBtn.Source = "arrow_down_yl.png";
            }
        }

        public void expandPermInfo(object sender, System.EventArgs e)
        {
            var mi = ((Label)sender);
            if(mi.LineBreakMode == LineBreakMode.TailTruncation) {
                mi.LineBreakMode = LineBreakMode.WordWrap;
                mi.MaxLines = int.MaxValue;
            } else {
                mi.LineBreakMode = LineBreakMode.TailTruncation;
                mi.MaxLines = 1;
            }
        }
   }
}