using System;
using System.Collections.Generic;

namespace ClearData.Models
{
    public class Company : DataObject
    {
        public enum RestrictionType { ALL = 0, CUSTOM = 1, NONE = 2}
        public SortedSet<int> WantedDataTypes { get; set; } //all data types wanted by the company, entry is the data type id
        
        //dictionary which maintains whether any data type has been enabled or disabled, if an entry doesn't exist, base it off
        //the Restriction setting
        public Dictionary<int, bool> DataTypeEnabled { get; set; } 

        //dictionary to store the last time any data was accessed by the company
        //null if company hasn't accessed the data type
        public Dictionary<int, DateTime> LastAccessed { get; set; }

        public RestrictionType Restriction
        {
            get => GetRestrictionType();
            set => SetRestrictionType(value);

        }

        private RestrictionType GetRestrictionType()
        {
            int onCount = 0;
            int totalCount = 0;
            foreach (KeyValuePair<int, bool> entry in DataTypeEnabled)
            {
                if (entry.Value == true)
                {
                    onCount += 1;
                }
                totalCount += 1;
            }
            if (totalCount == onCount)
            {
                return RestrictionType.ALL;
            } 
            else if (onCount == 0)
            {
                return RestrictionType.NONE;
            } 
            else
            {
                return RestrictionType.CUSTOM;
            }
        }

        private void SetRestrictionType(RestrictionType restriction)
        {
            if (restriction == RestrictionType.ALL)
            {
                foreach (KeyValuePair<int, bool> entry in DataTypeEnabled)
                {
                    DataTypeEnabled[entry.Key] = true;
                }
            } else if (restriction == RestrictionType.NONE)
            {
                foreach (KeyValuePair<int,bool> entry in DataTypeEnabled)
                {
                    DataTypeEnabled[entry.Key] = false;
                }
            }
        }
    }
}