using System.Collections.Generic;

namespace CCC_API.Data.Responses.Media.EdCal
{
    public class EdCals
    {
        public int ItemCount { get; set; }
        public List<EdCalsItem> Items { get; set; }
        public string Key { get; set; }
    }
}
