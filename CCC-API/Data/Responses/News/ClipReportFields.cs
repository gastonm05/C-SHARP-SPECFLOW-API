using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class ClipReportFields
    {
        public int ItemCount { get; set; }
        public List<ClipReportSingleField> Items { get; set; }
    }
}
