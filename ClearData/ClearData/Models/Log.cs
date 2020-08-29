using System;

namespace ClearData.Models
{
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