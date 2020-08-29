using System;
using System.Collections.ObjectModel;

namespace ClearData.Models
{
    //this is a really weird class which I need because I can't really work out how to iterate
    //over something like a map in xaml
    //either DataType or Company will be not null and should be known based on context
    //if there is a better way of doing this, I would love to hear it
    public class IndexedLogCollection
    {
        public DataType DataType { get; set; }
        public Company Company { get; set; }
        public ObservableCollection<Log> Logs { get; set;  }
    }
}