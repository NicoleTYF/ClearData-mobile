using System;
using System.Collections.Generic;

namespace ClearData.Models
{
    public class FineLocationLog : Log
    {
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}