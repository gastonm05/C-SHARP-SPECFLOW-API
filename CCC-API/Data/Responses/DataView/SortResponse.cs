using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.Responses.DataView
{
    public class SortResponse
    {
        public string Column { get; set; }
        public string Direction { get; set; }

        public SortResponse(string column, string direction)
        {
            this.Column = column;
            this.Direction = direction;
        }
    }
}
