using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClearData.Models;

namespace ClearData.Services
{
    public class PermissionsDataStore
    {
        public List<DataType> dataTypes;
        readonly List<Company> companies;

        public PermissionsDataStore()
        {

            dataTypes = new List<DataType>()
            {
                new DataType { Id = (int)DataType.DataTypeId.COARSE_LOCATION, Name = "Coarse Location Data", Description = "General location data tracking your rough location such as your current suburb", Enabled = false},
                new DataType { Id = (int)DataType.DataTypeId.FINE_LOCATION, Name = "Fine Location Data", Description = "GPS location data tracking your specific position, usually accurate to within 5 metres", Enabled = false },
                new DataType { Id = (int)DataType.DataTypeId.BROWSING, Name = "Browsing Data", Description = "Browsing history tracking what websites you visit", Enabled = false },
            };

            Company Google = new Company
            {
                Id = 0,
                Name = "Google",
                Description = "Google LLC is an American multinational technology" +
                "company that specializes in Internet-related services and products, which include online advertising technologies, " +
                "a search engine, cloud computing, software, and hardware. It is considered one of the Big Four technology companies " +
                "alongside Amazon, Apple and Microsoft.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.COARSE_LOCATION, (int)DataType.DataTypeId.BROWSING },
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.COARSE_LOCATION, new DateTime(2020, 8, 29, 15, 47, 53) },
                                                                    { (int)DataType.DataTypeId.BROWSING, new DateTime(2020, 8, 29, 15, 53, 41) }}
            };

            Company Amazon = new Company
            {
                Id = 1,
                Name = "Amazon",
                Description = "Amazon.com, Inc., is an American multinational technology company based in Seattle, Washington. " +
                "Amazon focuses on e-commerce, cloud computing, digital streaming, and artificial intelligence. It is considered one " +
                "of the Big Four technology companies, along with Google, Apple, and Facebook.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.FINE_LOCATION, (int)DataType.DataTypeId.BROWSING },
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.FINE_LOCATION, new DateTime(2020, 8, 29, 15, 49, 43) },
                                                                { (int)DataType.DataTypeId.BROWSING, new DateTime(2020, 8, 29, 15, 53, 41) }}
            };

            companies = new List<Company> { Google, Amazon }; //add the companies
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