using ClearData.Models;
using ClearData.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

using ClearData.Models;
using ClearData.Views;

namespace ClearData.ViewModels
{
    public class ManageDataViewModel : BaseViewModel
    {
        public ObservableCollection<DataType> DataTypes { get; }
        public ObservableCollection<Company> Companies { get; }
        public Command LoadDataTypesCommand { get; }
        public Command LoadCompaniesCommand { get; }

        public Command<Company> CompanyTapped { get; }

        /* whole bunch of variables for the button displays and which section to display
         * the SetProperty is important for updating the display so I think it needs to be this verbose
         */
        private bool servicesVisible;
        private bool dataTypesVisible;

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
        public ManageDataViewModel()
        {
            Title = "Manage Data";
            DataTypes = new ObservableCollection<DataType>();
            Companies = new ObservableCollection<Company>();
            LoadDataTypesCommand = new Command(async () => await ExecuteLoadDataTypesCommand());
            LoadCompaniesCommand = new Command(async () => await ExecuteLoadCompaniesCommand());
            CompanyTapped = new Command<Company>(OnCompanySelected);
            IsBusy = false;

            DataTypesVisible = true; //start with data visible, services not
            ServicesVisible = false;
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

        async void OnCompanySelected(Company company)
        {
            if (company == null)
                return;

            var manageCompanyViewModel = new ManageCompanyViewModel(company);

            await Application.Current.MainPage.Navigation.PushAsync(new ManageCompanyPage(manageCompanyViewModel));
            
        }

        public async void OnAppearing()
        {
            IsBusy = true;
            //when the page appears, load the data types or companies
            if (dataTypesVisible)
                await ExecuteLoadDataTypesCommand();
            else
                await ExecuteLoadCompaniesCommand(); 

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

    }
}