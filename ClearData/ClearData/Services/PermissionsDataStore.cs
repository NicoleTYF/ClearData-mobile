using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using ClearData.Models;
using Newtonsoft.Json;

namespace ClearData.Services
{
    public class PermissionsDataStore
    {
        public List<DataType> dataTypes;
        private List<Company> companies;
        private HashSet<(int, int)> enabledSet; //if (dataTypeId, companyId) in this set, then it is enabled

        public PermissionsDataStore()
        {
            
        }

        public async Task LoadDataStore()
        {
            /*
            dataTypes = new List<DataType>()
            {
                new DataType { Id = (int)DataType.DataTypeId.LOCATION, Name = "Location", Description = "Data tracking your current location"},
                new DataType { Id = (int)DataType.DataTypeId.BROWSING, Name = "Browsing", Description = "Browsing history tracking what websites you visit"},
                new DataType { Id = (int)DataType.DataTypeId.PHONE_USAGE, Name = "Phone Usage", Description = "Phone usage including app and extension usage, when you use them, who you use them with and your phone battery"},
                new DataType { Id = (int)DataType.DataTypeId.PAYMENT_HISTORY, Name = "Payments", Description = "Payments that you make online"},
                new DataType { Id = (int)DataType.DataTypeId.PHOTOS, Name = "Photos", Description = "Photos on your camera roll"},
                new DataType { Id = (int)DataType.DataTypeId.ADVERTISING, Name = "Advertising", Description = "Your interactions with advertisements including which ones you engage with"}
            };

            Company Google = new Company
            {
                Id = 0,
                Name = "Google",
                Description = "Google LLC is an American multinational technology" +
                "company that specializes in Internet-related services and products, which include online advertising technologies, " +
                "a search engine, cloud computing, software, and hardware. It is considered one of the Big Four technology companies " +
                "alongside Amazon, Apple and Microsoft.",
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.LOCATION, (int)DataType.DataTypeId.BROWSING, (int)DataType.DataTypeId.PHONE_USAGE,
                                                        (int)DataType.DataTypeId.PHOTOS, (int)DataType.DataTypeId.ADVERTISING}
            };

            Company Amazon = new Company
            {
                Id = 1,
                Name = "Amazon",
                Description = "Amazon.com, Inc., is an American multinational technology company based in Seattle, Washington. " +
                "Amazon focuses on e-commerce, cloud computing, digital streaming, and artificial intelligence. It is considered one " +
                "of the Big Four technology companies, along with Google, Apple, and Facebook.",
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.LOCATION, (int)DataType.DataTypeId.BROWSING, (int)DataType.DataTypeId.PHONE_USAGE, 
                                                        (int)DataType.DataTypeId.PAYMENT_HISTORY, (int)DataType.DataTypeId.ADVERTISING }
            };

            Company Spotify = new Company
            {
                Id = 2,
                Name = "Spotify",
                Description = "Spotify is a Swedish music streaming and media services provider. It is operated by Spotify AB, " +
                "which is publicly traded in the NYSE through Luxembourg-domiciled holding company Spotify Technology S.A., " +
                "itself a constituent of the Russell 1000 Index.",
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.BROWSING, (int)DataType.DataTypeId.LOCATION, 
                                                        (int)DataType.DataTypeId.PHONE_USAGE, (int)DataType.DataTypeId.PHOTOS}
            };

            Company Mozilla = new Company
            {
                Id = 3,
                Name = "Mozilla",
                Description = "Mozilla is a free software community founded in 1998 by members of Netscape. The Mozilla community uses, develops, spreads and " +
                "supports Mozilla products, thereby promoting exclusively free software and open standards, with only minor exceptions. " +
                "The community is supported institutionally by the not-for-profit Mozilla Foundation and its tax-paying subsidiary, the Mozilla Corporation.",
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.LOCATION, (int)DataType.DataTypeId.BROWSING,
                                                        (int)DataType.DataTypeId.ADVERTISING }
            };

            Company Uber = new Company
            {
                Id = 4,
                Name = "Uber",
                Description = "Uber Technologies, Inc., commonly known as Uber, offers vehicles for hire, food delivery (Uber Eats), package delivery, couriers, " +
                "freight transportation, and, through a partnership with Lime, electric bicycle and motorized scooter rental. The company is based in San " +
                "Francisco and has operations in over 900 metropolitan areas worldwide. It is one of the largest providers in the gig economy and is also a pioneer " +
                "in the development of self-driving cars.",
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.LOCATION, (int)DataType.DataTypeId.PHONE_USAGE,
                                                        (int)DataType.DataTypeId.PHOTOS }
            };

            Company Ebay = new Company
            {
                Id = 5,
                Name = "eBay",
                Description = "eBay Inc. is an American multinational e-commerce corporation based in San Jose, California, that facilitates consumer-to-consumer" +
                " and business-to-consumer sales through its website. eBay was founded by Pierre Omidyar in 1995, and became a notable success story of the dot-com " +
                "bubble. eBay is a multibillion-dollar business with operations in about 32 countries, as of 2019.",
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.BROWSING,
                                                        (int)DataType.DataTypeId.PAYMENT_HISTORY, (int)DataType.DataTypeId.ADVERTISING }
            };

            Company LinkedIn = new Company
            {
                Id = 6,
                Name = "LinkedIn",
                Description = "LinkedIn is an American business and employment-oriented online service that operates via websites and mobile apps. " +
                "Launched on May 5, 2003, it is mainly used for professional networking, including employers posting jobs and job seekers posting their CVs. " +
                "As of 2015, most of the company's revenue came from selling access to information about its members to recruiters and sales professionals. " +
                "Since December 2016 it has been a wholly owned subsidiary of Microsoft. As of May 2020, LinkedIn had 706 million registered members in 150 countries.",
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.PHOTOS, (int)DataType.DataTypeId.LOCATION }
            };

            Company Microsoft = new Company
            {
                Id = 7,
                Name = "Microsoft",
                Description = "Microsoft Corporation is an American multinational technology company with headquarters in Redmond, Washington. It develops, " +
                "manufactures, licenses, supports, and sells computer software, consumer electronics, personal computers, and related services. " +
                "Its best known software products are the Microsoft Windows line of operating systems, the Microsoft Office suite, and the Internet Explorer " +
                "and Edge web browsers. Its flagship hardware products are the Xbox video game consoles and the Microsoft Surface lineup of touchscreen personal computers.",
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.LOCATION, (int)DataType.DataTypeId.BROWSING, 
                                                        (int)DataType.DataTypeId.PHONE_USAGE, (int)DataType.DataTypeId.ADVERTISING }
            };

            Company Facebook = new Company
            {
                Id = 8,
                Name = "Facebook",
                Description = "Facebook is an American online social media and social networking service based in Menlo Park, California and a flagship service " +
                "of the namesake company Facebook, Inc. It was founded by Mark Zuckerberg, along with fellow Harvard College students and roommates " +
                "Eduardo Saverin, Andrew McCollum, Dustin Moskovitz and Chris Hughes.",
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.LOCATION, (int)DataType.DataTypeId.BROWSING, (int)DataType.DataTypeId.PHONE_USAGE,
                    (int)DataType.DataTypeId.PAYMENT_HISTORY, (int)DataType.DataTypeId.ADVERTISING, (int)DataType.DataTypeId.PHOTOS }
            };
            companies = new List<Company> { Google, Amazon, Spotify, Mozilla, Uber, Ebay, LinkedIn, Microsoft, Facebook }; //add the companies
            */

            enabledSet = new HashSet<(int, int)>();

            //load data types information
            HttpResponseMessage dataTypesResponse = await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.DATATYPES, 
                                                                                        DatabaseInteraction.HttpRequestType.GET, null);
            if (dataTypesResponse != null)
            {
                var jsonString = await dataTypesResponse.Content.ReadAsStringAsync();
                dataTypes = JsonConvert.DeserializeObject<List<DataType>>(jsonString);
            }

            //load company information
            HttpResponseMessage companiesResponse = await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.ENTERPRISES,
                                                                                        DatabaseInteraction.HttpRequestType.GET, null);
            if (companiesResponse != null)
            {
                var jsonString = await companiesResponse.Content.ReadAsStringAsync();
                companies = JsonConvert.DeserializeObject<List<Company>>(jsonString);
            }

            foreach (Company company in companies)
            {
                company.WantedDataTypes = new SortedSet<int>();
            }
            //get the wanted data types of all the companies
            HttpResponseMessage wantedDataTypesResponse = await DatabaseInteraction.SendDatabaseRequest(DatabaseInteraction.DatabaseRequest.WANTED_DATA_TYPES,
                                                                                        DatabaseInteraction.HttpRequestType.GET, null);
            if (wantedDataTypesResponse != null)
            {
                var jsonString = await wantedDataTypesResponse.Content.ReadAsStringAsync();
                List<IdPair> idPairs = JsonConvert.DeserializeObject<List<IdPair>>(jsonString);
                foreach (IdPair idPair in idPairs)
                {
                    //now I constructed all this structure in such a way that I never thought I would have to do this, but here we go!
                    //iterating through all of them is sad, but what are you going to do?
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

        }

        public bool InEnabledSet(int dataTypeId, int companyId)
        {
            return enabledSet.Contains((dataTypeId, companyId));
        }

        public void SetEnabled(int dataTypeId, int companyId, bool setting)
        {
            if (setting)
            {
                enabledSet.Add((dataTypeId, companyId));
            } 
            else
            {
                enabledSet.Remove((dataTypeId, companyId));
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

        //infer whether a data type is enabled from all the internal values
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
            new BasicLog{data_type=1, enterprise=1, time=DateTime.Now - new TimeSpan(0, 0, 13) },
            new BasicLog{data_type=1, enterprise=2, time=DateTime.Now - new TimeSpan(0, 1, 13) },
            new BasicLog{data_type=2, enterprise=1, time=DateTime.Now - new TimeSpan(0, 2, 13) },
            new BasicLog{data_type=2, enterprise=3, time=DateTime.Now - new TimeSpan(0, 3, 13) },
            new BasicLog{data_type=2, enterprise=4, time=DateTime.Now - new TimeSpan(1, 0, 13) },
            new BasicLog{data_type=3, enterprise=1, time=DateTime.Now - new TimeSpan(4, 0, 13) },
            new BasicLog{data_type=3, enterprise=4, time=DateTime.Now - new TimeSpan(5, 0, 13) },
            new BasicLog{data_type=3, enterprise=4, time=DateTime.Now - new TimeSpan(90, 0, 13) },
        };

        public Dictionary<(int,int),List<DateTime>> RetrieveAllRelevantLogs()
        {
            //this part will be replaced by a database call which will handle all the filtering
            //at the moment just pretend that we don't care about permissions

            Dictionary<(int, int), List<DateTime>> dict = new Dictionary<(int, int), List<DateTime>>();

            //go through all the return values and create a dictionary, which will make finding these things much easier
            foreach (var basicLog in ExampleLogs) {
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