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
using ClearData.Views;

namespace ClearData.ViewModels
{
	public class LogsViewModel: SwitchingViewModel
	{

        public ObservableCollection<IndexedLogCollection> TypeSortedLogs { get; set; }
        public ObservableCollection<IndexedLogCollection> CompanySortedLogs { get; set; }
        public Command<IndexedLogCollection> DataTypeHistoryBtnCommand { get; }
        public Command<IndexedLogCollection> CompanyHistoryBtnCommand { get; }
        public Command LoadDataTypeLogsCommand { get; }

        public LogsViewModel ()
	    {
            Title = "Logs";
            LoadDataTypeLogsCommand = new Command(async () => await ExecuteLoadDataTypesCommand());
            TypeSortedLogs = new ObservableCollection<IndexedLogCollection>();
            CompanySortedLogs = new ObservableCollection<IndexedLogCollection>();

            DataTypeHistoryBtnCommand = new Command<IndexedLogCollection>(OnDataTypeHistoryButtonSelected);
            CompanyHistoryBtnCommand = new Command<IndexedLogCollection>(OnCompanyHistoryButtonSelected);
        }


        async void OnDataTypeHistoryButtonSelected(IndexedLogCollection indexedLogCollection)
        {
            if (indexedLogCollection == null)
                return;

            LogHistoryDataTypeViewModel logHistoryDataTypeViewModel = new LogHistoryDataTypeViewModel(indexedLogCollection.DataType, indexedLogCollection.Logs);

            await Application.Current.MainPage.Navigation.PushAsync(new LogHistoryDataTypePage(logHistoryDataTypeViewModel));
        }

        async void OnCompanyHistoryButtonSelected(IndexedLogCollection indexedLogCollection)
        {
            if (indexedLogCollection == null)
                return;

            LogHistoryCompanyViewModel logHistoryCompanyViewModel = new LogHistoryCompanyViewModel(indexedLogCollection.Company, indexedLogCollection.Logs);

            await Application.Current.MainPage.Navigation.PushAsync(new LogHistoryCompanyPage(logHistoryCompanyViewModel));
        }

        public override async Task ExecuteLoadDataTypesCommand()
        {
            IsDataTypeDisplayBusy = true;
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
                        TypeSortedLogs.Add(new IndexedLogCollection() { DataType = dataType, Logs = new ObservableCollection<Log>(), 
                            LogsWithMaxElements = new ObservableCollection<Log>()});
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
                //and only add up to MAX_ELEMENTS into the other log list
                foreach (var typeSortedLog in TypeSortedLogs)
                {
                    ObservableCollection<Log> temp;
                    temp = new ObservableCollection<Log>(typeSortedLog.Logs.OrderByDescending(log => log.Time));
                    typeSortedLog.Logs.Clear();
                    typeSortedLog.LogsWithMaxElements.Clear();
                    int count = 0;
                    foreach (Log log in temp)
                    {
                        typeSortedLog.Logs.Add(log);
                        //have a cap on the number which can be added to this extra display list
                        if (count < IndexedLogCollection.MAX_ELEMENTS)
                        {
                            count++;
                            typeSortedLog.LogsWithMaxElements.Add(log);
                        }
                    }
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
                //first check the permissions data store exists
                if (UserInfo.permissions == null)
                {
                    UserInfo.permissions = new PermissionsDataStore();
                }
                CompanySortedLogs.Clear(); //clear the list

                //go through all the companies and create their
                var dataTypes = await UserInfo.permissions.GetDataTypesAsync(true);
                var companies = await UserInfo.permissions.GetCompaniesAsync(true);
                foreach (var company in companies)
                {
                    if (company.Restriction == Company.RestrictionType.NONE)
                        continue; //this company is restricted, don't display anything for the company
                    IndexedLogCollection thisIndexedLog = new IndexedLogCollection() { Company = company, Logs = new ObservableCollection<Log>(), 
                        LogsWithMaxElements = new ObservableCollection<Log>() };
                    CompanySortedLogs.Add(thisIndexedLog);
                    foreach (var dataType in dataTypes)
                    {
                        //if there is an entry to be made, find where to enter it
                        //for an entry to be made, it must be enabled by the user and the company and have a last accessed time
                        ManageCompanyViewModel.EnsureDataTypeEnabledEntry(company, dataType); //ensure the entry exists
                        if (dataType.Enabled && company.DataTypeEnabled[dataType.Id] &&
                            company.LastAccessed.TryGetValue(dataType.Id, out DateTime result))
                        {
                            thisIndexedLog.Logs.Add(new Log(dataType, company, company.LastAccessed[dataType.Id]));
                        }
                    }
                }

                //now because we wouldn't want to be finished, we need to sort each of the log collections by time
                //and only add up to MAX_ELEMENTS into the other log list
                foreach (var companySortedLog in CompanySortedLogs)
                {
                    ObservableCollection<Log> temp;
                    temp = new ObservableCollection<Log>(companySortedLog.Logs.OrderByDescending(log => log.Time));
                    companySortedLog.Logs.Clear();
                    companySortedLog.LogsWithMaxElements.Clear();
                    int count = 0;
                    foreach (Log log in temp)
                    {
                        companySortedLog.Logs.Add(log);
                        //have a cap on the number which can be added to this extra display list
                        if (count < IndexedLogCollection.MAX_ELEMENTS)
                        {
                            count++;
                            companySortedLog.LogsWithMaxElements.Add(log);
                        }
                    }
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
    }
}