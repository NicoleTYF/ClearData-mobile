﻿using System;

namespace ClearData.Models
{
    /**
     * A pair of Ids relating to the data type and enterprise. This is used for setting a datatype to be
     * enabled for a company from the perspective of the active user.
     */
    public class IdPair
    {
        public int enterprise { get; set; }
        public int data_type { get; set; }

        public override bool Equals(object obj)
        {
            return this.Equals(obj as IdPair);
        }

        public override int GetHashCode()
        {
            var hashCode = 352033288;
            hashCode = hashCode * -1521 * enterprise.GetHashCode();
            hashCode = hashCode * -7623 * data_type.GetHashCode();
            return hashCode;
        }

        private bool Equals(IdPair otherPair)
        {
            return this.enterprise == otherPair.enterprise && this.data_type == otherPair.data_type;
        }
    }
}