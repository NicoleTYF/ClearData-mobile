using ClearData.Services;
using System;
using System.Collections.Generic;

namespace ClearData.Models
{
    public class Company : DataObject
    {
        public enum RestrictionType { ALL = 0, CUSTOM = 1, NONE = 2}
        public SortedSet<int> WantedDataTypes { get; set; } //all data types wanted by the company, entry is the data type id

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
            int onCount = 0;
            int totalCount = 0;
            foreach (DataType dataType in UserInfo.GetPermissions().GetWantedDataTypesOverlap(this))
            {
                if (UserInfo.GetPermissions().InEnabledSet(dataType.Id, this.Id))
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

        /**
         * Set the restriction type by updating all the values, custom makes no change
         * This needs to only go through the data types which 
         */
        private void SetRestrictionType(RestrictionType restriction)
        {
            PermissionsDataStore permissions = UserInfo.GetPermissions();
            if (restriction == RestrictionType.ALL)
            {
                foreach (DataType dataType in permissions.GetWantedDataTypesOverlap(this))
                {
                    permissions.SetEnabled(dataType.Id, this.Id, true);
                }
            } else if (restriction == RestrictionType.NONE)
            {
                foreach (DataType dataType in permissions.GetWantedDataTypesOverlap(this))
                {
                    permissions.SetEnabled(dataType.Id, this.Id, false);
                }
            }
        }
    }
}