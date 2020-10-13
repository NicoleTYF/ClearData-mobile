using System;

namespace ClearData.Models
{
    /**
     * Companies and datatypes have quite an overlap, so they both inherit from this DataObject class
     */
    public class DataObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}