using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.Responses.DataView
{
    public class DataViewResponse
    {
        public string GridViewId { get; set; }
        public SortResponse Sort{ get; set; }

        public DataViewResponse(string gridViewId, SortResponse sort)
        {
            this.GridViewId = gridViewId;
            this.Sort = sort;
        }
        public DataViewResponse() { }
    }
}
