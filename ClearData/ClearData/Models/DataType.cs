using System;

namespace ClearData.Models
{
    public class DataType : DataObject
    {
        public enum DataTypeId { COARSE_LOCATION = 0, FINE_LOCATION = 1, BROWSING = 2}
        public bool Enabled { get; set; } //whether or not this data type is currently enabled
    }
}