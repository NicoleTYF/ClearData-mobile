using System;
using System.Collections.ObjectModel;

namespace ClearData.Models
{
    /**
     * class for the display of logs (database accesses), which is used in the Logs page.
     * this is a really weird class which I need because I can't really work out how to iterate over something like a map in xaml 
     * either DataType or Company will be not null and should be known based on context, ideally would collapse these both to DataObject 
     * types BUT we have pictures associated with companies, which means the distinction is necessary and having two classes is worse
     */
    public class IndexedLogCollection
    {
        //the max number of elements going in LogsWithMaxElements, caps logs page display
        public static int MAX_ELEMENTS = 3; 
        public DataType DataType { get; set; }
        public Company Company { get; set; }
        public ObservableCollection<Log> Logs { get; set;  }
        public ObservableCollection<Log> LogsWithMaxElements { get; set; } //when doing the display, we want a fixed number displayed
    }
}