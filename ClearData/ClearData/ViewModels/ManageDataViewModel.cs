using ClearData.Models;
using ClearData.Services;
using System;
using System.Collections.ObjectModel;
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

        }

        async Task ExecuteLoadDataTypesCommand()
        {
            IsBusy = true;
            /*
            try
            {
                DataTypes.Clear();
                var dataTypes = await PermissionsDataStore.GetDataTypesAsync(true);
                //UP TO HERE
            }
            */
        }

        public ICommand OpenWebCommand { get; }
    }
}