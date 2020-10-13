using System;

namespace ClearData.Models
{
    /**
     * A log to be displayed, this is different to a BasicLog in that it contains the objects not
     * just the Ids
     */
    public class Log
    {
        public DataType DataType { get; set; }
        public Company Company { get; set; }
        public DateTime Time { get; set; }
        public Log(DataType dataType, Company company, DateTime time)
        {
            DataType = dataType;
            Company = company;
            Time = time;
        }
    }
}