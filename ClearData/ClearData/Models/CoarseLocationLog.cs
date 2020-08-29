using System;
using System.Collections.Generic;

namespace ClearData.Models
{
    //not sure exactly what would be in a coarse location log
    public class CoarseLocationLog : Log
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}