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

        public ManageCompanyViewModel(Company company)
        {
            Title = String.Format("Manage {0} permissions", company.Name);
            Company = company;
            DataTypePermissions = new ObservableCollection<CompanyDataType>();
            //get the permission options as a list
            RestrictionChanged = new Command(OnRestrictionChanged);
            LoadPermissionsCommand = new Command(async () => await ExecuteLoadPermissionsCommand());
            CurrentRestriction = (int)company.Restriction; //need a variable for this, as it won't sync directly
        }

        public void OnRestrictionChanged()
        {
            //we have had the restriction changed, update the actual restriction
            company.Restriction = (Company.RestrictionType)currentRestriction;
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
                        if (!company.DataTypeEnabled.TryGetValue(dataType.Id, out bool result))
                        {
                            //if we are in here, there wasn't an entry in the dictionary
                            //add the entry in the dictionary depending on the current restriction setting
                            switch(company.Restriction)
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

    }
}
