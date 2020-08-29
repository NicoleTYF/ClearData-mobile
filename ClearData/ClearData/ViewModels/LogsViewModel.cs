using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;

using ClearData.Models;
using ClearData.Services;

namespace ClearData.ViewModels
{
	public class LogsViewModel: BaseViewModel
	{

        public ObservableCollection<IndexedLogCollection> TypeSortedLogs { get; set; }
        public Command HistoryBtnCommand { get; }
        public Command LoadLogsCommand { get; }

        

        public LogsViewModel ()
	    {
            LoadLogsCommand = new Command(async () => await ExecuteLoadLogsCommand());
            TypeSortedLogs = new ObservableCollection<IndexedLogCollection>();

            // HistoryBtnCommand = new Command(OnHistoryBtnTapped);
        }

        public void OnAppearing()
        {
            IsBusy = true;
        }

        async Task ExecuteLoadLogsCommand()
        {
            IsBusy = true;
            try
            {
                //first check the permissions data store exists
                if (UserInfo.permissions == null)
                {
                    UserInfo.permissions = new PermissionsDataStore();
                }
                TypeSortedLogs.Clear(); //clear the list

                //then create all the observable lists of logs, one for each datatype that is being tracked
                var dataTypes = await UserInfo.permissions.GetDataTypesAsync(true);
                foreach (var dataType in dataTypes)
                {
                    if (dataType.Enabled)
                    {
                        TypeSortedLogs.Add(new IndexedLogCollection() { DataType = dataType, Logs = new ObservableCollection<Log>()});
                    }
                }
                //now that we have the creations for each of the datatypes, go through all the companies and add all their usage
                var companies = await UserInfo.permissions.GetCompaniesAsync(true);
                foreach (var company in companies)
                {
                    foreach (var dataType in dataTypes)
                    {
                        //if there is an entry to be made, find where to enter it
                        //for an entry to be made, it must be enabled by the user and the company and have a last accessed time
                        ManageCompanyViewModel.EnsureDataTypeEnabledEntry(company, dataType); //ensure the entry exists
                        if (dataType.Enabled && company.DataTypeEnabled[dataType.Id] &&
                            company.LastAccessed.TryGetValue(dataType.Id, out DateTime result))
                        {
                            //this hurts my soul a bit with the remarkable inefficiency, but we won't be doing anything big
                            //so it is kinda fine, and to do it better I would need to track indices and stuff
                            foreach (var typeSortedLog in TypeSortedLogs)
                            {
                                if (typeSortedLog.DataType.Id == dataType.Id)
                                {
                                    typeSortedLog.Logs.Add(new Log(dataType, company, company.LastAccessed[dataType.Id]));
                                    break;
                                }
                            }
                        }
                    }
                }
                
                //now because we wouldn't want to be finished, we need to sort each of the log collections by time
                foreach (var typeSortedLog in TypeSortedLogs)
                {
                    ObservableCollection<Log> temp;
                    temp = new ObservableCollection<Log>(typeSortedLog.Logs.OrderByDescending(log => log.Time));
                    typeSortedLog.Logs.Clear();
                    foreach (Log log in temp)
                    {
                        typeSortedLog.Logs.Add(log);
                    }
                }
                Console.WriteLine("YELLOW {0} {1}", TypeSortedLogs.Count, TypeSortedLogs[0].Logs.Count);
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