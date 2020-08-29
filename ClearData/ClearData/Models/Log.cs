using System;
using System.Collections.Generic;

namespace ClearData.Models
{
    public class Log
    {
        public int Id { get; set; }
        public int DataTypeId { get; set; } //ID of the associated data type
        public DateTime LogTime { get; set; } //Timestamp associated with each log
    }
}