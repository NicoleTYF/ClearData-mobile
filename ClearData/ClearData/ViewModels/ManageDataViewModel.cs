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
    public class ManageDataViewModel : SwitchingViewModel
    {
        public ObservableCollection<DataType> DataTypes { get; }
        public ObservableCollection<Company> Companies { get; }
        public Command LoadDataTypesCommand { get; }
        public Command LoadCompaniesCommand { get; }

        public Command<Company> CompanyTapped { get; }


        public ManageDataViewModel() : base()
        {
            Title = "Manage Data";
            DataTypes = new ObservableCollection<DataType>();
            Companies = new ObservableCollection<Company>();
            LoadDataTypesCommand = new Command(async () => await ExecuteLoadDataTypesCommand());
            LoadCompaniesCommand = new Command(async () => await ExecuteLoadCompaniesCommand());
            CompanyTapped = new Command<Company>(OnCompanySelected);
            IsBusy = false;
        }

        public override async Task ExecuteLoadDataTypesCommand()
        {
            IsDataTypeDisplayBusy = true;

            try
            {
                //then we clear the observable collection and replace it
                DataTypes.Clear();
                var dataTypes = await UserInfo.GetPermissions().GetDataTypesAsync(true);
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
                IsDataTypeDisplayBusy = false;
            }
            
        }

        public override async Task ExecuteLoadCompaniesCommand()
        {
            IsServicesDisplayBusy = true;

            try
            {
                //then we clear the observable collection and replace it
                Companies.Clear();
                var companies = await UserInfo.GetPermissions().GetCompaniesAsync(true);
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
                IsServicesDisplayBusy = false;
            }

        }

        async void OnCompanySelected(Company company)
        {
            if (company == null)
                return;

            var manageCompanyViewModel = new ManageCompanyViewModel(company);

            await Application.Current.MainPage.Navigation.PushAsync(new ManageCompanyPage(manageCompanyViewModel));
            
        }
    }
}