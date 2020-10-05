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
        public DateTime time { get; set; }
        public double price { get; set; }

    }
}