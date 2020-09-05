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
                new DataType { Id = (int)DataType.DataTypeId.PHONE_USAGE, Name = "Phone Usage", Description = "Phone usage including app and extension usage, when you use them, who you use them with and your phone battery", Enabled=false},
                new DataType { Id = (int)DataType.DataTypeId.PAYMENT_HISTORY, Name = "Payments", Description = "Payments that you make online", Enabled=false},
                new DataType { Id = (int)DataType.DataTypeId.PHOTOS, Name = "Photos", Description = "Photos on your camera roll", Enabled=false},
                new DataType { Id = (int)DataType.DataTypeId.ADVERTISING, Name = "Advertising", Description = "Your interactions with advertisements including which ones you engage with", Enabled=false}
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
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.COARSE_LOCATION, (int)DataType.DataTypeId.BROWSING, (int)DataType.DataTypeId.PHONE_USAGE,
                                                        (int)DataType.DataTypeId.PHOTOS, (int)DataType.DataTypeId.ADVERTISING},
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.COARSE_LOCATION, DateTime.Now - new TimeSpan(500, 0, 0) },
                                                                    { (int)DataType.DataTypeId.BROWSING, DateTime.Now - new TimeSpan(0, 14, 13) },
                                                                    { (int)DataType.DataTypeId.PHONE_USAGE, DateTime.Now - new TimeSpan(28, 0, 0) },
                                                                    { (int)DataType.DataTypeId.PHOTOS, DateTime.Now - new TimeSpan(100, 2, 0) },
                                                                    { (int)DataType.DataTypeId.ADVERTISING, DateTime.Now }}
            };

            Company Amazon = new Company
            {
                Id = 1,
                Name = "Amazon",
                Description = "Amazon.com, Inc., is an American multinational technology company based in Seattle, Washington. " +
                "Amazon focuses on e-commerce, cloud computing, digital streaming, and artificial intelligence. It is considered one " +
                "of the Big Four technology companies, along with Google, Apple, and Facebook.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.FINE_LOCATION, (int)DataType.DataTypeId.BROWSING, (int)DataType.DataTypeId.PHONE_USAGE, 
                                                        (int)DataType.DataTypeId.PAYMENT_HISTORY, (int)DataType.DataTypeId.ADVERTISING },
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.FINE_LOCATION, DateTime.Now - new TimeSpan(2, 26, 13) },
                                                                    { (int)DataType.DataTypeId.BROWSING, DateTime.Now - new TimeSpan(42, 2, 13) },
                                                                    { (int)DataType.DataTypeId.PHONE_USAGE, DateTime.Now - new TimeSpan(1, 0, 0) },
                                                                    { (int)DataType.DataTypeId.PAYMENT_HISTORY, DateTime.Now - new TimeSpan(9, 2, 0) },
                                                                    { (int)DataType.DataTypeId.ADVERTISING, DateTime.Now - new TimeSpan(25, 2, 0) }}
            };

            Company Spotify = new Company
            {
                Id = 2,
                Name = "Spotify",
                Description = "Spotify is a Swedish music streaming and media services provider. It is operated by Spotify AB, " +
                "which is publicly traded in the NYSE through Luxembourg-domiciled holding company Spotify Technology S.A., " +
                "itself a constituent of the Russell 1000 Index.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.BROWSING, (int)DataType.DataTypeId.COARSE_LOCATION, 
                                                        (int)DataType.DataTypeId.PHONE_USAGE, (int)DataType.DataTypeId.PHOTOS},
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.COARSE_LOCATION, DateTime.Now - new TimeSpan(3, 42, 13) },
                                                                    { (int)DataType.DataTypeId.BROWSING, DateTime.Now - new TimeSpan(0, 8, 13) },
                                                                    { (int)DataType.DataTypeId.PHONE_USAGE, DateTime.Now - new TimeSpan(4, 19, 0) },
                                                                    { (int)DataType.DataTypeId.PHOTOS, DateTime.Now - new TimeSpan(0, 35, 0) }}
            };

            Company Mozilla = new Company
            {
                Id = 3,
                Name = "Mozilla",
                Description = "Mozilla is a free software community founded in 1998 by members of Netscape. The Mozilla community uses, develops, spreads and " +
                "supports Mozilla products, thereby promoting exclusively free software and open standards, with only minor exceptions. " +
                "The community is supported institutionally by the not-for-profit Mozilla Foundation and its tax-paying subsidiary, the Mozilla Corporation.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.COARSE_LOCATION, (int)DataType.DataTypeId.BROWSING,
                                                        (int)DataType.DataTypeId.ADVERTISING },
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.COARSE_LOCATION, DateTime.Now - new TimeSpan(0, 2, 13) },
                                                                    { (int)DataType.DataTypeId.BROWSING, DateTime.Now - new TimeSpan(85, 2, 13) },
                                                                    { (int)DataType.DataTypeId.ADVERTISING, DateTime.Now - new TimeSpan(600, 4, 0) }}
            };

            Company Uber = new Company
            {
                Id = 4,
                Name = "Uber",
                Description = "Uber Technologies, Inc., commonly known as Uber, offers vehicles for hire, food delivery (Uber Eats), package delivery, couriers, " +
                "freight transportation, and, through a partnership with Lime, electric bicycle and motorized scooter rental. The company is based in San " +
                "Francisco and has operations in over 900 metropolitan areas worldwide. It is one of the largest providers in the gig economy and is also a pioneer " +
                "in the development of self-driving cars.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.FINE_LOCATION, (int)DataType.DataTypeId.PHONE_USAGE,
                                                        (int)DataType.DataTypeId.PHOTOS },
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.FINE_LOCATION, DateTime.Now - new TimeSpan(67, 26, 13) },
                                                                    { (int)DataType.DataTypeId.PHONE_USAGE, DateTime.Now - new TimeSpan(1, 0, 0) },
                                                                    { (int)DataType.DataTypeId.PHOTOS, DateTime.Now}}
            };

            Company Ebay = new Company
            {
                Id = 5,
                Name = "eBay",
                Description = "eBay Inc. is an American multinational e-commerce corporation based in San Jose, California, that facilitates consumer-to-consumer" +
                " and business-to-consumer sales through its website. eBay was founded by Pierre Omidyar in 1995, and became a notable success story of the dot-com " +
                "bubble. eBay is a multibillion-dollar business with operations in about 32 countries, as of 2019.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.BROWSING,
                                                        (int)DataType.DataTypeId.PAYMENT_HISTORY, (int)DataType.DataTypeId.ADVERTISING },
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.PAYMENT_HISTORY, DateTime.Now - new TimeSpan(14, 26, 13) },
                                                                    { (int)DataType.DataTypeId.BROWSING, DateTime.Now - new TimeSpan(1400, 2, 13) },
                                                                    { (int)DataType.DataTypeId.ADVERTISING, DateTime.Now - new TimeSpan(300, 2, 0) }}
            };

            Company LinkedIn = new Company
            {
                Id = 6,
                Name = "LinkedIn",
                Description = "LinkedIn is an American business and employment-oriented online service that operates via websites and mobile apps. " +
                "Launched on May 5, 2003, it is mainly used for professional networking, including employers posting jobs and job seekers posting their CVs. " +
                "As of 2015, most of the company's revenue came from selling access to information about its members to recruiters and sales professionals. " +
                "Since December 2016 it has been a wholly owned subsidiary of Microsoft. As of May 2020, LinkedIn had 706 million registered members in 150 countries.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.PHOTOS, (int)DataType.DataTypeId.COARSE_LOCATION },
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.PHOTOS, DateTime.Now - new TimeSpan(700, 26, 13) },
                                                                    { (int)DataType.DataTypeId.COARSE_LOCATION, DateTime.Now - new TimeSpan(400, 2, 0) }}
            };

            Company Microsoft = new Company
            {
                Id = 7,
                Name = "Microsoft",
                Description = "Microsoft Corporation is an American multinational technology company with headquarters in Redmond, Washington. It develops, " +
                "manufactures, licenses, supports, and sells computer software, consumer electronics, personal computers, and related services. " +
                "Its best known software products are the Microsoft Windows line of operating systems, the Microsoft Office suite, and the Internet Explorer " +
                "and Edge web browsers. Its flagship hardware products are the Xbox video game consoles and the Microsoft Surface lineup of touchscreen personal computers.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.FINE_LOCATION, (int)DataType.DataTypeId.BROWSING, 
                                                        (int)DataType.DataTypeId.PHONE_USAGE, (int)DataType.DataTypeId.ADVERTISING },
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.FINE_LOCATION, DateTime.Now },
                                                                    { (int)DataType.DataTypeId.BROWSING, DateTime.Now - new TimeSpan(2, 2, 13) },
                                                                    { (int)DataType.DataTypeId.PHONE_USAGE, DateTime.Now - new TimeSpan(5, 0, 0) },
                                                                    { (int)DataType.DataTypeId.ADVERTISING, DateTime.Now - new TimeSpan(10, 2, 0) }}
            };

            Company Facebook = new Company
            {
                Id = 8,
                Name = "Facebook",
                Description = "Facebook is an American online social media and social networking service based in Menlo Park, California and a flagship service " +
                "of the namesake company Facebook, Inc. It was founded by Mark Zuckerberg, along with fellow Harvard College students and roommates " +
                "Eduardo Saverin, Andrew McCollum, Dustin Moskovitz and Chris Hughes.",
                Restriction = Company.RestrictionType.ALL,
                WantedDataTypes = new SortedSet<int> { (int)DataType.DataTypeId.FINE_LOCATION, (int)DataType.DataTypeId.BROWSING, (int)DataType.DataTypeId.PHONE_USAGE,
                                                        (int)DataType.DataTypeId.PAYMENT_HISTORY, (int)DataType.DataTypeId.ADVERTISING, (int)DataType.DataTypeId.PHOTOS },
                DataTypeEnabled = new Dictionary<int, bool>(),
                LastAccessed = new Dictionary<int, DateTime>() { { (int)DataType.DataTypeId.FINE_LOCATION, DateTime.Now - new TimeSpan(0, 50, 13) },
                                                                    { (int)DataType.DataTypeId.BROWSING, DateTime.Now - new TimeSpan(0, 4, 13) },
                                                                    { (int)DataType.DataTypeId.PHONE_USAGE, DateTime.Now - new TimeSpan(0, 5, 45) },
                                                                    { (int)DataType.DataTypeId.PAYMENT_HISTORY, DateTime.Now - new TimeSpan(0, 9, 0) },
                                                                    { (int)DataType.DataTypeId.PHOTOS, DateTime.Now - new TimeSpan(0, 2, 0) },
                                                                    { (int)DataType.DataTypeId.ADVERTISING, DateTime.Now - new TimeSpan(0, 42, 0) }}
            };

            companies = new List<Company> { Google, Amazon, Spotify, Mozilla, Uber, Ebay, LinkedIn, Microsoft, Facebook }; //add the companies
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