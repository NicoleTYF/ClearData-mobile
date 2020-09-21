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
        public Command LoadCompanyLogsCommand { get; }

        public LogsViewModel () : base()
	    {
            Title = "Logs";
            LoadDataTypeLogsCommand = new Command(async () => await ExecuteLoadDataTypesCommand());
            LoadCompanyLogsCommand = new Command(async () => await ExecuteLoadCompaniesCommand());
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
                TypeSortedLogs.Clear(); //clear the list

                //go through each of the datatypes and work out all the company accesses for each
                var dataTypes = await UserInfo.GetPermissions().GetDataTypesAsync(true);
                var companies = await UserInfo.GetPermissions().GetCompaniesAsync(true);
                var logDictionary = UserInfo.GetPermissions().RetrieveAllRelevantLogs();
                foreach (DataType dataType in dataTypes)
                {
                    //create an indexed log collection, create this for all datatypes, but only add it to the TypeSortedLogs if it has an entry
                    IndexedLogCollection dataTypeLogCollection = new IndexedLogCollection()
                    {
                        DataType = dataType,
                        Logs = new ObservableCollection<Log>(),
                        LogsWithMaxElements = new ObservableCollection<Log>()
                    };
                    bool toAdd = false; //whether to add this indexed log collection to the full list, set true if there is a log added
                    foreach (Company company in companies)
                    {
                        if (logDictionary.ContainsKey((dataType.Id, company.Id)))
                        {
                            toAdd = true; //there was at least one entry, this data type should be displayed
                            //there is a list, iterate through it and add all the 
                            foreach (DateTime dateTime in logDictionary[(dataType.Id, company.Id)])
                            {
                                dataTypeLogCollection.Logs.Add(new Log(dataType, company, dateTime));
                            }
                        }
                    }
                    if (toAdd)
                    {
                        TypeSortedLogs.Add(dataTypeLogCollection);
                    }
                }

                SortAndTrimEntries(TypeSortedLogs);
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
                CompanySortedLogs.Clear(); //clear the list

                //go through each of the companies and work out all the data type accesses for each
                var dataTypes = await UserInfo.GetPermissions().GetDataTypesAsync(true);
                var companies = await UserInfo.GetPermissions().GetCompaniesAsync(true);
                var logDictionary = UserInfo.GetPermissions().RetrieveAllRelevantLogs();
                foreach (Company company in companies)
                {
                    //create an indexed log collection, create this for all datatypes, but only add it to the TypeSortedLogs if it has an entry
                    IndexedLogCollection companyLogCollection = new IndexedLogCollection()
                    {
                        Company = company,
                        Logs = new ObservableCollection<Log>(),
                        LogsWithMaxElements = new ObservableCollection<Log>()
                    };
                    bool toAdd = false; //whether to add this indexed log collection to the full list, set true if there is a log added
                    foreach (DataType dataType in dataTypes)
                    {
                        if (logDictionary.ContainsKey((dataType.Id, company.Id)))
                        {
                            toAdd = true; //there was at least one entry, this data type should be displayed
                            //there is a list, iterate through it and add all the 
                            foreach (DateTime dateTime in logDictionary[(dataType.Id, company.Id)])
                            {
                                companyLogCollection.Logs.Add(new Log(dataType, company, dateTime));
                            }
                        }
                    }
                    if (toAdd)
                    {
                        CompanySortedLogs.Add(companyLogCollection);
                    }
                }

                SortAndTrimEntries(CompanySortedLogs);
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


        /**
         * takes all of the logs which have been added, and makes sure that they are all sorted, and the LogsWithMaxElements
         * are created containing the first MAX_ELEMENTS entries
         */
        private void SortAndTrimEntries(ObservableCollection<IndexedLogCollection> sortedLogs)
        {
            foreach (var logCollection in sortedLogs)
            {
                ObservableCollection<Log> temp;
                temp = new ObservableCollection<Log>(logCollection.Logs.OrderByDescending(log => log.Time));
                logCollection.Logs.Clear();
                logCollection.LogsWithMaxElements.Clear();
                int count = 0;
                foreach (Log log in temp)
                {
                    logCollection.Logs.Add(log);
                    //have a cap on the number which can be added to this extra display list
                    if (count < IndexedLogCollection.MAX_ELEMENTS)
                    {
                        count++;
                        logCollection.LogsWithMaxElements.Add(log);
                    }
                }
            }
        }
    }
}