using System;
using System.Collections.Generic;

namespace ClearData.Models
{
    public class Company : DataObject
    {
        public enum RestrictionType {ALL = 0, CUSTOM = 1, NONE = 2}; //Possible types of restrictions
        public SortedSet<int> WantedDataTypes { get; set; } //all data types wanted by the company, entry is the data type id
        
        public RestrictionType Restriction { get; set; } 
        
        //dictionary which maintains whether any data type has been enabled or disabled, if an entry doesn't exist, base it off
        //the Restriction setting
        public Dictionary<int, bool> DataTypeEnabled { get; set; } 

        //dictionary to store the last time any data was accessed by the company
        //null if company hasn't accessed the data type
        public Dictionary<int, DateTime> LastAccessed { get; set; }
    }
}