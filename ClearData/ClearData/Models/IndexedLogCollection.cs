using System;
using System.Collections.ObjectModel;

namespace ClearData.Models
{
    //this is a really weird class which I need because I can't really work out how to iterate
    //over something like a map in xaml
    //either DataType or Company will be not null and should be known based on context, ideally would collapse these both to DataObject
    //types BUT eventually we might have pictures associated with companies, which would mean it would be important to have this

    //if there is a better way of doing this, I would love to hear it
    public class IndexedLogCollection
    {
        //the max number of elements going in LogsWithMaxElements, caps logs page display
        public static int MAX_ELEMENTS = 3; 
        public DataType DataType { get; set; }
        public Company Company { get; set; }
        public ObservableCollection<Log> Logs { get; set;  }
        public ObservableCollection<Log> LogsWithMaxElements { get; set; }
    }
}