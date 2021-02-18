using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.Outlet
{
    public class Outlets
    {
        public int ItemCount { get; set; }
        public List<OutletsItem> Items { get; set; }
        public string Key { get; set; }
    }
}
