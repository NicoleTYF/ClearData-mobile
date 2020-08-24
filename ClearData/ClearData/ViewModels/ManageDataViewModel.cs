using ClearData.Models;
using ClearData.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ClearData.ViewModels
{
    public class ManageDataViewModel : BaseViewModel
    {
        public ObservableCollection<DataType> DataTypes { get; }
        public ObservableCollection<Company> Companies { get; }
        public Command LoadDataTypesCommand { get; }
        public Command LoadCompaniesCommand { get; }

        /* whole bunch of variables for the button displays and which section to display
         * the SetProperty is important for updating the display so I think it needs to be this verbose
         */
        private Color serviceButtonColor;
        private Color dataTypeButtonColor;
        private Color serviceButtonSecondary;
        private Color dataTypeButtonSecondary;
        private bool servicesVisible;
        private bool dataTypesVisible;

        public Color ServiceButtonColor
        {
            get => serviceButtonColor;
            set => SetProperty(ref serviceButtonColor, value);
        }
        public Color DataTypeButtonColor
        {
            get => dataTypeButtonColor;
            set => SetProperty(ref dataTypeButtonColor, value);
        }
        public Color ServiceButtonSecondary
        {
            get => serviceButtonSecondary;
            set => SetProperty(ref serviceButtonSecondary, value);
        }
        public Color DataTypeButtonSecondary
        {
            get => dataTypeButtonSecondary;
            set => SetProperty(ref dataTypeButtonSecondary, value);
        }
        public bool ServicesVisbile
        {
            get => servicesVisible;
            set => SetProperty(ref servicesVisible, value);
        }
        public bool DataTypesVisible
        {
            get => dataTypesVisible;
            set => SetProperty(ref dataTypesVisible, value);
        }
        public ManageDataViewModel()
        {
            Title = "Manage Data";
            DataTypes = new ObservableCollection<DataType>();
            Companies = new ObservableCollection<Company>();
            LoadDataTypesCommand = new Command(async () => await ExecuteLoadDataTypesCommand());
            LoadCompaniesCommand = new Command(async () => await ExecuteLoadCompaniesCommand());
            IsBusy = false;

            DataTypesVisible = false; //ensures the next function runs fine
            UpdateToDataTypesDisplay();
        }

        async Task ExecuteLoadDataTypesCommand()
        {
            IsBusy = true;

            try
            {
                //first check to see if the permissions object exists. I am a little unsure where we are instantiating
                //these static variables, potentially this needs to move somewhere else
                if (UserInfo.permissions == null)
                {
                    UserInfo.permissions = new PermissionsDataStore();
                }
                //then we clear the observable collection and replace it
                DataTypes.Clear();
                var dataTypes = await UserInfo.permissions.GetDataTypesAsync(true);
                foreach (var dataType in dataTypes)
                {
                    DataTypes.Add(dataType);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
            
        }

        async Task ExecuteLoadCompaniesCommand()
        {
            IsBusy = true;

            try
            {
                //first check to see if the permissions object exists. I am a little unsure where we are instantiating
                //these static variables, potentially this needs to move somewhere else
                if (UserInfo.permissions == null)
                {
                    UserInfo.permissions = new PermissionsDataStore();
                }
                //then we clear the observable collection and replace it
                Companies.Clear();
                var companies = await UserInfo.permissions.GetCompaniesAsync(true);
                foreach (var company in companies)
                {
                    Companies.Add(company);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }

        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        /**
         * Update the variables which are used to display the button colours and which view to make
         * visible, this is called when the services button is pressed
         */
        public void UpdateToServicesDisplay()
        {
            if (servicesVisible) { return; } //already set, don't need to reset every button push
            //all these colours are hardcoded here, probably want to base them on some theme in the future
            ServiceButtonColor = Color.White;
            ServiceButtonSecondary = Color.CornflowerBlue;
            DataTypeButtonColor = Color.DarkGray;
            DataTypeButtonSecondary = Color.LightGray;
            DataTypesVisible = false;
            ServicesVisbile = true;
        }

        /**
         * Update the variables which are used to display the button colours and which view to make
         * visible, this is called when the services button is pressed, this is also default so called upon
         * instantiation. Set DataTypesVisible false before calling for instantiation
         */
        public void UpdateToDataTypesDisplay()
        {
            if (DataTypesVisible) { return; } //already set, bail
            //all these colours are hardcoded here, probably want to base them on some theme in the future
            DataTypeButtonColor = Color.White;
            DataTypeButtonSecondary = Color.CornflowerBlue;
            ServiceButtonColor = Color.DarkGray;
            ServiceButtonSecondary = Color.LightGray;
            ServicesVisbile = false;
            DataTypesVisible = true;
        }

        public ICommand OpenWebCommand { get; }
    }
}