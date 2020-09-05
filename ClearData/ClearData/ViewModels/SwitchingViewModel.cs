using ClearData.Models;
using ClearData.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using ClearData.Views;

namespace ClearData.ViewModels
{
    public abstract class SwitchingViewModel : BaseViewModel
    {

        /* whole bunch of variables for the button displays and which section to display
         * the SetProperty is important for updating the display so I think it needs to be this verbose
         */
        private bool servicesVisible;
        private bool dataTypesVisible;
        bool isDataTypeDisplayBusy = false; //add a different IsBusy parameter for each to stop infinite loops
        bool isServicesDisplayBusy = false;

        public bool ServicesVisible
        {
            get => servicesVisible;
            set => SetProperty(ref servicesVisible, value);
        }
        public bool DataTypesVisible
        {
            get => dataTypesVisible;
            set => SetProperty(ref dataTypesVisible, value);
        }
        
        public bool IsDataTypeDisplayBusy
        {
            get { return isDataTypeDisplayBusy; }
            set { SetProperty(ref isDataTypeDisplayBusy, value); }
        }
        
        public bool IsServicesDisplayBusy
        {
            get { return isServicesDisplayBusy; }
            set { SetProperty(ref isServicesDisplayBusy, value); }
        }
        public SwitchingViewModel()
        {
            DataTypesVisible = true; //start with data visible, services not
            ServicesVisible = false;
        }

        /**
         * Update the variables which are used to display the button colours and which view to make
         * visible, this is called when the services button is pressed
         */
        public async Task UpdateToServicesDisplay()
        {
            if (servicesVisible) { return; } //already set, don't need to reset every button push
            DataTypesVisible = false;
            ServicesVisible = true;
            IsBusy = true;
            await ExecuteLoadCompaniesCommand(); //reload the companies when we switch to this view
        }

        /**
         * Update the variables which are used to display the button colours and which view to make
         * visible, this is called when the services button is pressed, this is also default so called upon
         * instantiation. Set DataTypesVisible false before calling for instantiation
         */
        public async Task UpdateToDataTypesDisplay()
        {
            if (DataTypesVisible) { return; } //already set, bail
            ServicesVisible = false;
            DataTypesVisible = true;
            IsBusy = true;
            await ExecuteLoadDataTypesCommand(); //reload the data type info
        }

        public async void OnAppearing()
        {
            //when the page appears, load the data types or companies, only need to load one
            if (DataTypesVisible)
                await ExecuteLoadDataTypesCommand();
            else
                await ExecuteLoadCompaniesCommand();

        }

        public abstract Task ExecuteLoadDataTypesCommand(); //function to run when switching to the data type display
        public abstract Task ExecuteLoadCompaniesCommand(); //function to run when switching to the companies display

    }
}