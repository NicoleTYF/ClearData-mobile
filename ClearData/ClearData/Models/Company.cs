using System;
using System.Collections.Generic;

namespace ClearData.Models
{
    public class Company : DataObject
    {
        public enum RestrictionType {ALL = 0, CUSTOM_OPT_IN = 1, CUSTOM_OPT_OUT = 2, NONE = 3}; //Possible types of restrictions
        public SortedSet<int> WantedDataTypes { get; set; } //all data types wanted by the company, entry is the data type id
        
        public RestrictionType Restriction { get; set; } 
        
        //dictionary which maintains whether any data type has been enabled or disabled, if an entry doesn't exist, base it off
        //the Restriction setting
        public Dictionary<int, bool> DataTypeEnabled { get; set; } 
    }
}