using System;
using System.Collections.Generic;

namespace ClearData.Models
{
    public class Company : DataObject
    {
        public enum RestrictionType {ALL, CUSTOM_OPT_IN, CUSTOM_OPT_OUT, NONE }; //Possible types of restrictions
        public SortedSet<int> WantedDataTypes { get; set; } //all data types wanted by the company, entry is the data type id
        public RestrictionType Restriction { get; set; } 
        public SortedSet<int> ExcludedDataTypes { get; set; } //set of all data types which have permissions excluded, entry is the data type id
    }
}