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
        public Command LoadDataTypesCommand { get; }
        public ManageDataViewModel()
        {
            Title = "Manage Data";
            DataTypes = new ObservableCollection<DataType>();
            LoadDataTypesCommand = new Command(async () => await ExecuteLoadDataTypesCommand());
            IsBusy = false;

            System.Console.WriteLine("HELLO FRIENDS");
        }

        async Task ExecuteLoadDataTypesCommand()
        {
            IsBusy = true;
            System.Console.WriteLine("ORANGE");

            try
            {
                //first check to see if the permissions object exists. I am a little unsure where we are instantiating
                //these static variables, potentially this needs to move somewhere else
                if (UserInfo.permissions == null)
                {
                    System.Console.WriteLine("INSIDE");
                    UserInfo.permissions = new PermissionsDataStore();
                }
                System.Console.WriteLine("OUTSIDE");
                //then we clear the observable collection and replace it
                DataTypes.Clear();
                var dataTypes = await UserInfo.permissions.GetDataTypesAsync(true);
                foreach (var dataType in dataTypes)
                {
                    DataTypes.Add(dataType);
                }
                System.Console.WriteLine("HELLOOO %d", DataTypes.ToString());
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

        public ICommand OpenWebCommand { get; }
    }
}