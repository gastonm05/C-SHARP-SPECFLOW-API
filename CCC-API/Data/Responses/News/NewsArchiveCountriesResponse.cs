using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CCC_API.Data.Responses.News
{
    public class NewsArchiveCountriesResponse
    {
        public int ItemCount { get; set; }
        public List<ClipReportSingleField> Items { get; set; }
    }
}
