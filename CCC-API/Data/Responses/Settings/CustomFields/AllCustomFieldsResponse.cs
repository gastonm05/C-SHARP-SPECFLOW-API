using System.Collections.Generic;
using CCC_API.Data.PostData.Settings.CustomFields;

namespace CCC_API.Data.Responses.Settings.CustomFields
{
    public class AllCustomFieldsResponse
    {
        public int itemsCount { get; set; }
        public CustomFieldsPostData items { get; set; }      
    }

    public class AvaliableCustomFields
    {
        public int ItemCount { get; set; }
        public List<CustomFieldsPostData> Items { get; set; }
        public Common.Links _links { get; set; }
    }
}
