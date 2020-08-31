using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ClearData.Models;
using Xamarin.Forms;

using ClearData.Services;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ClearData.ViewModels
{
    public class ManageCompanyViewModel : BaseViewModel
    {
        
        public ObservableCollection<CompanyDataType> DataTypePermissions { get; }

        private int currentRestriction;
        private Company company;
        public int CurrentRestriction 
        { 
            get => currentRestriction; 
            set => SetProperty(ref currentRestriction, value); //done like this as it is bound
        }
        
        public Company Company
        {
            get => company;
            set => SetProperty(ref company, value);
        }
        
        public Command RestrictionChanged { get; }
        public Command LoadPermissionsCommand { get; }
        public Command BackButtonPressed { get; }

        public ManageCompanyViewModel(Company company)
        {
            Title = String.Format("Manage {0} permissions", company.Name);
            Company = company;
            DataTypePermissions = new ObservableCollection<CompanyDataType>();
            //get the permission options as a list
            RestrictionChanged = new Command(OnRestrictionChanged);
            CurrentRestriction = (int)company.Restriction; //need a variable for this, as it won't sync directly
            LoadPermissionsCommand = new Command(async () => await ExecuteLoadPermissionsCommand());
            BackButtonPressed = new Command(async () => await ExecuteBackButtonPressed());
        }



        public void OnRestrictionChanged()
        {
            //we have had the restriction changed, update the actual restriction
            company.Restriction = (Company.RestrictionType)currentRestriction;

            //next update the sliders and permissions to reflect this
            //only done if changing to ALL or NONE
            if (company.Restriction == Company.RestrictionType.ALL || 
                company.Restriction == Company.RestrictionType.NONE)
            {
                for (int i = 0; i < DataTypePermissions.Count; i++)
                {
                    CompanyDataType currentCompanyDataType = DataTypePermissions[i];
                    CompanyDataType newCompanyDataType = new CompanyDataType()
                    {
                        DataType = currentCompanyDataType.DataType,
                        CompanyEnabled = (company.Restriction == Company.RestrictionType.ALL)
                    };
                    //then update the entry in the data type permissions collection
                    //since it is an observable collection, need to update the full object rather than just the CompanyEnabled field
                    DataTypePermissions[i] = newCompanyDataType;
                    //this will trigger the SwitchToggled event for all of the switches, which updates the backend
                    //I couldn't work out how to change the display without triggering that function a whole bunch of times
                    //so it may run a few times but is fairly quick so should be fine
                }
            }
        }

        public static void EnsureDataTypeEnabledEntry(Company company, DataType dataType)
        {
            if (!company.DataTypeEnabled.TryGetValue(dataType.Id, out bool result))
            {
                //if we are in here, there wasn't an entry in the dictionary
                //add the entry in the dictionary depending on the current restriction setting
                switch (company.Restriction)
                {
                    case Company.RestrictionType.ALL:
                    case Company.RestrictionType.CUSTOM_OPT_OUT:
                        company.DataTypeEnabled.Add(dataType.Id, true);
                        break;
                    default:
                        company.DataTypeEnabled.Add(dataType.Id, false);
                        break;
                }
            }
        }
            
        async Task ExecuteBackButtonPressed()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        async Task ExecuteLoadPermissionsCommand()
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
                DataTypePermissions.Clear();
                var dataTypes = await UserInfo.permissions.GetDataTypesAsync(true); //get the data types
                foreach (var dataType in dataTypes)
                {
                    //first check for overlap between the wanted data types and the enabled data types and only display those
                    if (dataType.Enabled && company.WantedDataTypes.Contains(dataType.Id))
                    {
                        EnsureDataTypeEnabledEntry(Company, dataType);
                        //now make an entry into the tuple observable collection which we use for our display
                        DataTypePermissions.Add(new CompanyDataType()
                        {
                            DataType = dataType,
                            CompanyEnabled = company.DataTypeEnabled[dataType.Id]
                        });
                    }
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

        public void SwitchToggled()
        {
            //don't worry about tracking which switch was actually toggled
            //since we want to iterate through all switches anyway to
            //check for changing the overall permission
            //this can also be triggered by the permission being changed so needs to be safe from that
            int trueCount = 0;
            foreach (var companyDataType in DataTypePermissions)
            {
                //first do the updating of the backend to reflect the change to the switch
                //switch bound to company enabled, not the dictionary
                company.DataTypeEnabled[companyDataType.DataType.Id] = companyDataType.CompanyEnabled;
                //next increment the true count, this is counting the new status
                if (company.DataTypeEnabled[companyDataType.DataType.Id])
                    trueCount++;
            }
            //now use the number of trues counted to update the restriction type
            //first check updating to ALL setting, don't do if on CUSTOM_OPT_IN setting or already on ALL
            if (trueCount == DataTypePermissions.Count &&
                    company.Restriction != Company.RestrictionType.CUSTOM_OPT_IN &&
                    company.Restriction != Company.RestrictionType.ALL)
            {
                company.Restriction = Company.RestrictionType.ALL;
            }
            else if (trueCount == 0 && company.Restriction != Company.RestrictionType.NONE)
            {
                company.Restriction = Company.RestrictionType.NONE;
            }
            //then update if we are now in the middle but came from one of the ends
            else if (trueCount < DataTypePermissions.Count && trueCount > 0)
            {
                if (company.Restriction == Company.RestrictionType.NONE)
                {
                    //coming from none restriction, change to opt in
                    company.Restriction = Company.RestrictionType.CUSTOM_OPT_IN;
                }
                else if (company.Restriction == Company.RestrictionType.ALL)
                {
                    //coming from all restriction, change to opt out
                    company.Restriction = Company.RestrictionType.CUSTOM_OPT_OUT;
                }
            }
            //finally update the restriction that is being displayed
            CurrentRestriction = (int)company.Restriction;
        }

    }
}
