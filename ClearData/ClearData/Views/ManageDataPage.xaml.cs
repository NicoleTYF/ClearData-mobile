﻿using System;
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

        private async void OnServicePushed(object sender, EventArgs e)
        {
             await _viewModel.UpdateToServicesDisplay();
        }

        private async void OnDataTypesPushed(object sender, EventArgs e)
        {
            await _viewModel.UpdateToDataTypesDisplay();
        }

        private void OpenInfo(object sender, System.EventArgs e)
        {
            DisplayAlert("How to use", "Select 'Service' to enable/disable data individually, \n\nSelect 'Data types' to enable/disable data types shared to all service providers.", "Got it");
        }

        public void expandPermInfo(object sender, System.EventArgs e)
        {
            var permDesc = ((Label)sender);
            if (permDesc.LineBreakMode == LineBreakMode.TailTruncation) {
                permDesc.LineBreakMode = LineBreakMode.WordWrap;
                permDesc.MaxLines = int.MaxValue;
            } else {
                permDesc.LineBreakMode = LineBreakMode.TailTruncation;
                permDesc.MaxLines = 1;
            }
        }
    }
}