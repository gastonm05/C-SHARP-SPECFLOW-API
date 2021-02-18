
using System;
using System.Collections.Generic;

namespace CCC_API.Data.Responses.Grid
{
    public class Column
    {
        public string Name { get; set; }
        public string Sort { get; set; }
        public int Order { get; set; }
        public bool Visibility { get; set; }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Sort)}: {Sort}, {nameof(Order)}: {Order}, {nameof(Visibility)}: {Visibility}";
        }
    }

    // Custom comparer for the Product class
    public class ColumnComparer : IEqualityComparer<Column>
    {
        // Products are equal if their names and product numbers are equal.
        public bool Equals(Column x, Column y)
        {
            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.Name == y.Name && x.Order == y.Order && x.Sort == y.Sort && x.Visibility == y.Visibility ;
        }
        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(Column column)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(column, null)) return 0;

            //Get hash code for the column field if it is not null.
            int hashColumn = column == null ? 0 : column.GetHashCode();
            
            return hashColumn;
        }

    }
}
