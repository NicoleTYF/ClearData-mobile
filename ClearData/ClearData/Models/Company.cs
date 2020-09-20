﻿using System;
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

        /**
         * Infer the restriction type from the current enabled values, if they are all on then its ALL
         * if none on, NONE, if inbetween, CUSTOM
         */
        private RestrictionType GetRestrictionType()
        {
            Console.WriteLine("YELLOW!");
            int onCount = 0;
            int totalCount = 0;
            foreach (DataType dataType in UserInfo.GetPermissions().GetWantedDataTypesOverlap(this))
            {
                if (DataTypeEnabled[dataType.Id] == true)
                {
                    onCount += 1;
                }
                totalCount += 1;
            }
            Console.WriteLine(String.Format("YELLOWW {0} {1}", onCount, totalCount));
            if (totalCount == onCount)
            {
                Console.WriteLine("yellow 1");
                return RestrictionType.ALL;
            } 
            else if (onCount == 0)
            {
                Console.WriteLine("yellow 2");
                return RestrictionType.NONE;
            } 
            else
            {
                Console.WriteLine("yellow 3");
                return RestrictionType.CUSTOM;
            }
        }

        /**
         * Set the restriction type by updating all the values, custom makes no change
         * This needs to only go through the data types which 
         */
        private void SetRestrictionType(RestrictionType restriction)
        {
            if (restriction == RestrictionType.ALL)
            {
                foreach (DataType dataType in UserInfo.GetPermissions().GetWantedDataTypesOverlap(this))
                {
                    DataTypeEnabled[dataType.Id] = true;
                }
            } else if (restriction == RestrictionType.NONE)
            {
                foreach (DataType dataType in UserInfo.GetPermissions().GetWantedDataTypesOverlap(this))
                {
                    DataTypeEnabled[dataType.Id] = false;
                }
            }
        }
    }
}