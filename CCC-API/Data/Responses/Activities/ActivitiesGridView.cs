using System.Collections.Generic;
using CCC_API.Data.Responses.Grid;

namespace CCC_API.Data.Responses.Activities
{
    public class ActivitiesGridView
    {
        public List<Column> Columns { get; set; }
        public string GridViewId { get; set; }
    }
}
