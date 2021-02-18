using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsCustomFields
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public bool MultiSelect { get; set; }
        public List<UDFAllowedValues> AllowedValues { get; set; }
    }
}
