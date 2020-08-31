using System;

namespace ClearData.Models
{
    /**
     * This data type is really annoying to need, but I don't think there is any way to pass a tuple to
     * the xaml x:dataType, which means that these data types with one extra boolean need their own class, grrr
     */
    public class CompanyDataType
    {
        public bool CompanyEnabled { get; set; } //whether or not this data type is currently enabled for this company
        public DataType DataType { get; set; }
    }
}