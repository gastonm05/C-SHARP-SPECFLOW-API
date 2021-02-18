using System.Collections.Generic;

namespace CCC_API.Data.Responses.News
{
    public class NewsCustomFieldPatchModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Edition { get; set; }
        public List<UDFAllowedValues> AllowedValues { get; set; } 
    }
}
