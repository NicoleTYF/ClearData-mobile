using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using ClearData.Models;
using Newtonsoft.Json;
using System.Text;

namespace ClearData.Services
{
    /**
     * The main data store for the entire app, contains information relating to the companies,
     * data types, permissions settings and logs. 
     */
    public class PermissionsDataStore
    {
        public List<DataType> dataTypes; // list of all data types
        public List<Company> companies; // list of all companies
        private HashSet<IdPair> enabledSet; // if (dataTypeId, companyId) in this set, then it is enabled

        /**
         * Load the data store. This requires the authentication token to have been generated as it
         * makes user specific database calls.
         */
        public async Task LoadDataStore()
        {
            // load data types information from database
            HttpResponseMessage dataTypesResponse = await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.DATATYPES, 
                                                                                        DatabaseInteraction.HttpRequestType.GET, null, true, true);
            if (dataTypesResponse != null)
            {
                var jsonString = await dataTypesResponse.Content.ReadAsStringAsync();
                dataTypes = JsonConvert.DeserializeObject<List<DataType>>(jsonString);
            }

            //load company information
            HttpResponseMessage companiesResponse = await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.ENTERPRISES,
                                                                                        DatabaseInteraction.HttpRequestType.GET, null, true, true);
            if (companiesResponse != null)
            {
                var jsonString = await companiesResponse.Content.ReadAsStringAsync();
                companies = JsonConvert.DeserializeObject<List<Company>>(jsonString);
            }

            // initialise all the wanted data types as empty
            foreach (Company company in companies)
            {
                company.WantedDataTypes = new SortedSet<int>();
            }
            // get the wanted data types of all the companies
            HttpResponseMessage wantedDataTypesResponse = await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.WANTED_DATA_TYPES,
                                                                                        DatabaseInteraction.HttpRequestType.GET, null, true, true);
            if (wantedDataTypesResponse != null)
            {
                var jsonString = await wantedDataTypesResponse.Content.ReadAsStringAsync();
                List<IdPair> idPairs = JsonConvert.DeserializeObject<List<IdPair>>(jsonString);
                foreach (IdPair idPair in idPairs)
                {
                    //now I constructed all this structure in such a way that I never thought I would have to do this, but here we go!
                    //iterating through all of them is sad, but it's necessary to play nicely with the database
                    foreach (Company company in companies)
                    {
                        if (company.Id == idPair.enterprise)
                        {
                            company.WantedDataTypes.Add(idPair.data_type);
                            break;
                        }
                    }
                }
            }

            // load the permissions into the enabled set
            HttpResponseMessage permissionsResponse = await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.USER_PERMISSIONS,
                                                                                        DatabaseInteraction.HttpRequestType.GET, null, true, true);
            enabledSet = new HashSet<IdPair>();
            if (permissionsResponse != null)
            {
                var jsonString = await permissionsResponse.Content.ReadAsStringAsync();
                // cannot deserialise straight into a hashset, so instead go to a list and then build the hashset
                // it is faster to have these in a hashset for use cases, so a one off conversion is worth it
                List<IdPair> idsList = JsonConvert.DeserializeObject<List<IdPair>>(jsonString);
                foreach (IdPair idPair in idsList)
                {
                    enabledSet.Add(idPair);
                }
            }

        }

        /**
         * Return true iff the (dataTypeId, companyId) tuple is in the set of enabled permissions, i.e. is enabled
         */
        public bool InEnabledSet(int dataTypeId, int companyId)
        {
            return enabledSet.Contains(new IdPair(){data_type=dataTypeId, enterprise=companyId });
        }

        /**
         * Sets whether a (dataTYpeId, companyId) tuple is enabled. This is done by hashset removal or addition, but
         * this is a useful abstraction
         */
        public async void SetEnabled(int dataTypeId, int companyId, bool setting)
        {
            IdPair idPair = new IdPair() { data_type = dataTypeId, enterprise = companyId };
            if (setting && !InEnabledSet(dataTypeId, companyId))
            {
                enabledSet.Add(idPair); //update the value locally
                //update the value on the server
                var jsonString = JsonConvert.SerializeObject(idPair);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.ALLOW_PERMISSION,
                            DatabaseInteraction.HttpRequestType.POST, jsonContent, true, true);
                //error checking done in the request
            } 
            else if (!setting && InEnabledSet(dataTypeId, companyId))
            {
                enabledSet.Remove(idPair);
                //update the value on the server
                var jsonString = JsonConvert.SerializeObject(idPair);
                var jsonContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
                await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.DENY_PERMISSION,
                            DatabaseInteraction.HttpRequestType.POST, jsonContent, true, true);
                //error checking done in the request
            }
        }

        /**
         * get the data types that the company wants, which overlap with the data types that are globally enabled
         */
        public List<DataType> GetWantedDataTypesOverlap(Company company)
        {
            List<DataType> wantedDataTypesOverlap = new List<DataType>();
            //for each data type, if it is wanted by the company
            foreach (DataType dataType in dataTypes)
            {
                if (company.WantedDataTypes.Contains(dataType.Id) && IsDataTypeEnabledGlobally(dataType.Id))
                {
                    wantedDataTypesOverlap.Add(dataType);
                }
            }
            return wantedDataTypesOverlap;
        } 

        /**
         * infer whether a data type is globally enabled from all the internal values
         */
        public bool IsDataTypeEnabledGlobally(int dataTypeId)
        {
            foreach (Company company in companies)
            {
                if (company.WantedDataTypes.Contains(dataTypeId) && InEnabledSet(dataTypeId, company.Id))
                {
                    return true;
                }
            }
            return false;
        }

        /**
         * Set a data type global permission. If this is a transition, enable/disable the data type for all companies.
         */
        public void SetDataTypeGlobalPermission(int dataTypeId, bool setting)
        {
            foreach (Company company in companies)
            {
                if (setting == false && company.WantedDataTypes.Contains(dataTypeId))
                {
                    SetEnabled(dataTypeId, company.Id, setting);
                }
                else if (setting == true && company.WantedDataTypes.Contains(dataTypeId))
                {
                    //update the settings, unless we are on the none setting for the company and are trying to switch it on
                    //this is a weird one, because once we update one of them, it then treats the data type as being on for determining
                    //whether the current setting is ALL or NONE, so we need to actually do that manually here, and not consider this
                    //data type
                    //so we want to set it on if there are any enabled permissions OR there are no other wanted data types that are permitted globally
                    bool companySetting = true;
                    foreach (int otherDataTypeId in company.WantedDataTypes)
                    {
                        if (otherDataTypeId != dataTypeId && InEnabledSet(otherDataTypeId, company.Id))
                        {
                            //this company must not be on the NONE setting, so we are good to set its permissions on
                            companySetting = true;
                            break;
                        }
                        else if (companySetting == true && otherDataTypeId != dataTypeId && IsDataTypeEnabledGlobally(otherDataTypeId))
                        {
                            companySetting = false; //there is at least one other data type which is globally enabled but not enabled for this company
                            //in this case, we set the flag to false, but we don't break because this can be overridden by finding one that is turned on
                            //if this is set false and we never fall into the first clause, then and only then will the setting be false
                        }
                    }
                    SetEnabled(dataTypeId, company.Id, companySetting);
                }
                //i am aware how horrificly inefficient this is, but I am trying to not save any information that isn't in the database, and its weird to do
            }
        }

        //this will get removed and replaced with a database call
        // tuple order is datatype then company
        private static readonly List<BasicLog> ExampleLogs = new List<BasicLog>
        {
            new BasicLog{data_type=1, enterprise=6, time=DateTime.Now - new TimeSpan(0, 0, 13), price=4.53 },
            new BasicLog{data_type=1, enterprise=5, time=DateTime.Now - new TimeSpan(0, 1, 13), price=2.34 },
            new BasicLog{data_type=2, enterprise=4, time=DateTime.Now - new TimeSpan(35*24, 2, 13), price=1.04 },
            new BasicLog{data_type=2, enterprise=3, time=DateTime.Now - new TimeSpan(0, 3, 13), price=8.32 },
            new BasicLog{data_type=2, enterprise=4, time=DateTime.Now - new TimeSpan(8*24, 0, 13), price=4.23 },
            new BasicLog{data_type=3, enterprise=5, time=DateTime.Now - new TimeSpan(4, 0, 13), price=0.23 },
            new BasicLog{data_type=3, enterprise=4, time=DateTime.Now - new TimeSpan(5, 0, 13), price=0.45 },
            new BasicLog{data_type=3, enterprise=4, time=DateTime.Now - new TimeSpan(90, 0, 13), price=0.32 },
        };

        public List<BasicLog> RetrieveAllLogsList()
        {
            return ExampleLogs;
        }

        /**
         * Retrieve all relevant logs as a ditionary linking (dataTypeId, companyId) pairs to lists of times
         * this is useful for the display of logs, as we need to group together all data types or companies
         * however this is not the way we want to actually store the logs, because that is not how they are used
         * to generate graphs, and a list is more useful in that situation.
         */
        public Dictionary<(int,int),List<DateTime>> RetrieveAllRelevantLogs()
        {
            //this part will be replaced by a database call which will handle all the filtering
            //at the moment just pretend that we don't care about permissions
            Dictionary<(int, int), List<DateTime>> dict = new Dictionary<(int, int), List<DateTime>>();

            //go through all the return values and create a dictionary, which will make finding these things much easier
            List<BasicLog> logList = ExampleLogs; //this will be replaced by a call to the database
            foreach (var basicLog in logList) {
                if (!dict.ContainsKey((basicLog.data_type, basicLog.enterprise)))
                {
                    dict[(basicLog.data_type, basicLog.enterprise)] = new List<DateTime>();
                }
                dict[(basicLog.data_type, basicLog.enterprise)].Add(basicLog.time);
            }
            return dict;
        }

        public async Task<IEnumerable<DataType>> GetDataTypesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(dataTypes);
        }

        public async Task<IEnumerable<Company>> GetCompaniesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(companies);
        }

        public async Task<Company> GetCompanyAsync(string name)
        {
            return await Task.FromResult(companies.FirstOrDefault(s => Equals(s.Name,name)));
        }
    }
}