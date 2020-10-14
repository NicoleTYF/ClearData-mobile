using System;

namespace ClearData.Models
{
    /**
     * basic log, contains just the ids of the datatype and company, not the actual object
     * this is what will get read into from the database
     */
    public class BasicLog
    {
        public int data_type { get; set; }
        public int enterprise { get; set; }
        public DateTime time { get; set; } //an actual date time object, used for actual timing
        public string date_accessed { get; set; } //a string for the date, the form the database sends
        public double price { get; set; }

    }
}