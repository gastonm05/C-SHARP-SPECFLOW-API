using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.PostData.Grids
{
    public class DataViewPostData
    {
        public string viewType { get; set; }

        public DataViewPostData() { }

        public DataViewPostData(string viewType)
        {
            this.viewType = viewType;
        }
    }
}
